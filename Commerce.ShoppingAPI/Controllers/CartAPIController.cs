using Azure;
using Commerce.ShoppingAPI.Entity;
using Commerce.ShoppingAPI.Models.Dto;
using Commerce.ShoppingAPI.Models.ResponseDto;
using Commerce.ShoppingAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Data;
using System.Linq.Expressions;

namespace Commerce.ShoppingAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private IProductService _productService;
        private ICouponService _couponService;

        public CartAPIController(AppDbContext db, IProductService productService, ICouponService couponService)
        {
            _db = db;
            _productService = productService;
            _couponService = couponService;

        }

        [HttpPost("CartUpsert")]
        public async Task<ResponseDto<CartDto>> Upsert(CartDto cartDto)
        {
            var response = new ResponseDto<CartDto>();

            try
            {
                var cartHeaderFromDb = await _db.CartHeaders/*.AsNoTracking()*/.FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    // Create header and details
                    CartHeader cartHeader = new CartHeader
                    {
                        UserId = cartDto.CartHeader.UserId,
                        CouponCode = cartDto.CartHeader.CouponCode
                    };
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();

                    var cartDetail = cartDto.CartDetails.First();
                    cartDetail.CartHeaderId = cartHeader.CartHeaderId;
                    CartDetails newCartDetails = new CartDetails
                    {
                        CartHeaderId = cartDetail.CartHeaderId,
                        ProductId = cartDetail.ProductId,
                        Count = cartDetail.Count
                    };
                    _db.CartDetails.Add(newCartDetails);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    // If header is not null, check if details have the same product
                    var cartDetailDto = cartDto.CartDetails.First();
                    var cartDetailsFromDb = await _db.CartDetails/*.AsNoTracking()*/.FirstOrDefaultAsync(
                        u => u.ProductId == cartDetailDto.ProductId &&
                        u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                    if (cartDetailsFromDb == null)
                    {
                        // Create cart details
                        cartDetailDto.CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        CartDetails newCartDetails = new CartDetails
                        {
                            CartHeaderId = cartDetailDto.CartHeaderId,
                            ProductId = cartDetailDto.ProductId,
                            Count = cartDetailDto.Count
                        };
                        _db.CartDetails.Add(newCartDetails);
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        // Update count in cart details
                        cartDetailsFromDb.Count += cartDetailDto.Count;
                        _db.CartDetails.Update(cartDetailsFromDb);
                        await _db.SaveChangesAsync();
                    }
                }
                response.Result = cartDto;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }

        [HttpDelete("CartRemove/{cartDetailsId}")]
        public async Task<ResponseDto<bool>> RemoveCartDetails(int cartDetailsId)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var cartDetailsFromDb = await _db.CartDetails.FindAsync(cartDetailsId);
                if (cartDetailsFromDb == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Cart details not found.";
                    response.Result = false;
                    return response;
                }

                var cartHeaderId = cartDetailsFromDb.CartHeaderId;
                _db.CartDetails.Remove(cartDetailsFromDb);
                await _db.SaveChangesAsync();

                // Check if there are any other details linked to this header
                var remainingDetails = await _db.CartDetails.AnyAsync(cd => cd.CartHeaderId == cartHeaderId);
                if (!remainingDetails)
                {
                    var cartHeaderFromDb = await _db.CartHeaders.FindAsync(cartHeaderId);
                    if (cartHeaderFromDb != null)
                    {
                        _db.CartHeaders.Remove(cartHeaderFromDb);
                        await _db.SaveChangesAsync();
                    }
                }

                response.IsSuccess = true;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                response.Result = false;
            }
            return response;
        }



        [HttpPost("ApplyCoupon")]
        public async Task<ResponseDto<bool>> ApplyCoupon([FromBody] CartDto cartDto)
        {
            var response = new ResponseDto<bool>();
            try
            {
                var cartFromDb = await _db.CartHeaders.FirstAsync(u => u.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                _db.CartHeaders.Update(cartFromDb);
                await _db.SaveChangesAsync();
                response.Result = true;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }


        [HttpPost("RemoveCoupon")]
        public async Task<ResponseDto<bool>> RemoveCoupon([FromBody] CartDto cartDto)
        {
            var response = new ResponseDto<bool>();
            try
            {
                var cartFromDb = await _db.CartHeaders.FirstAsync(u => u.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = "";
                _db.CartHeaders.Update(cartFromDb);
                await _db.SaveChangesAsync();
                response.Result = true;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }


        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto<CartDto>> GetCart(string userId)
        {
            var response = new ResponseDto<CartDto>();

            try
            {
                var cartHeader = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
                if (cartHeader == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Cart header not found.";
                    return response;
                }

                var cartDetails = await _db.CartDetails
                    .Where(u => u.CartHeaderId == cartHeader.CartHeaderId)
                    .ToListAsync();

                var cart = new CartDto
                {
                    CartHeader = new CartHeaderDto
                    {
                        CartHeaderId = cartHeader.CartHeaderId,
                        UserId = cartHeader.UserId,
                        CouponCode = cartHeader.CouponCode,
                        CartTotal = 0 // Initialize CartTotal
                    },
                    CartDetails = cartDetails.Select(detail => new CartDetailsDto
                    {
                        CartDetailsId = detail.CartDetailsId,
                        CartHeaderId = detail.CartHeaderId,
                        ProductId = detail.ProductId,
                        Count = detail.Count
                    }).ToList()
                };

                // Fetch product details
                var productDtos = await _productService.GetProducts();

                // Map product details and calculate cart total
                foreach (var item in cart.CartDetails)
                {
                    var product = productDtos.FirstOrDefault(p => p.ProductId == item.ProductId);
                    if (product != null)
                    {
                        item.Product = product;
                        cart.CartHeader.CartTotal += item.Count * product.Price;
                    }
                }

                //apply coupon if any
                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDto coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
                    if(coupon!=null && cart.CartHeader.CartTotal > coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount;
                        cart.CartHeader.Discount = coupon.DiscountAmount;
                    }
                }

                response.Result = cart;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }



    }
}
