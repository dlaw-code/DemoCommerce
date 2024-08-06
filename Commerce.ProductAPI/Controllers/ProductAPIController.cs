
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProductAPI.API.Data;
using Commerce.ProductAPI.Models.Dto;
using Commerce.ProductAPI.Entity;

namespace Commerce.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
   
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CouponAPIController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<ResponseDto<IEnumerable<ProductDto>>> Get()
        {
            var coupons = _db.Products
                .Select(c => new ProductDto
                {
                    ProductId = c.ProductId,
                    Description = c.Description,
                    Name = c.Name,
                    Price = c.Price,
                    ImageUrl = c.ImageUrl,
                    CategoryName = c.CategoryName,
                   
                })
                .ToList();

            return Ok(new ResponseDto<IEnumerable<ProductDto>>
            {
                Result = coupons,
                IsSuccess = true,
                Message = "Products retrieved successfully"
            });
        }

        [HttpGet("{id:int}")]
        public ActionResult<ResponseDto<ProductDto>> GetById(int id)
        {
            var product = _db.Products
                .Where(c => c.ProductId == id)
                .Select(c => new ProductDto
                {
                    ProductId = c.ProductId,
                    Description = c.Description,
                    Name = c.Name,
                    Price = c.Price,
                    ImageUrl = c.ImageUrl,
                    CategoryName = c.CategoryName,
                })
                .FirstOrDefault();

            if (product == null)
            {
                return NotFound(new ResponseDto<ProductDto>
                {
                    IsSuccess = false,
                    Message = "Product not found"
                });
            }

            return Ok(new ResponseDto<ProductDto>
            {
                Result = product,
                IsSuccess = true,
                Message = "Coupon retrieved successfully"
            });
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<ResponseDto<ProductDto>> Post([FromBody] ProductDto productDto)
        {
            var product = new Product
            {
                Description = productDto.Description,
                Name = productDto.Name,
                Price = productDto.Price,
                ImageUrl = productDto.ImageUrl,
                CategoryName = productDto.CategoryName,
                
            };

            _db.Products.Add(product);
            _db.SaveChanges();

            productDto.ProductId = product.ProductId;

            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, new ResponseDto<ProductDto>
            {
                Result = productDto,
                IsSuccess = true,
                Message = "Product created successfully"
            });
        }

        //[HttpGet("GetByCode/{code}")]
        //public async Task<ActionResult<ResponseDto<ProductDto>>> GetByCode(string code)
        //{
        //    var coupon = await _db.Coupons
        //        .Where(u => u.CouponCode.ToLower() == code.ToLower())
        //        .Select(c => new CouponDto
        //        {
        //            CouponId = c.CouponId,
        //            CouponCode = c.CouponCode,
        //            DiscountAmount = c.DiscountAmount,
        //            MinAmount = c.MinAmount
        //        })
        //        .FirstOrDefaultAsync();

        //    if (coupon == null)
        //    {
        //        return NotFound(new ResponseDto<CouponDto>
        //        {
        //            IsSuccess = false,
        //            Message = "Coupon not found"
        //        });
        //    }

        //    return Ok(new ResponseDto<CouponDto>
        //    {
        //        Result = coupon,
        //        IsSuccess = true,
        //        Message = "Coupon retrieved successfully"
        //    });
        //}

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<ResponseDto<ProductDto>> Put([FromBody] ProductDto productDto)
        {
            var existingProduct = _db.Products.FirstOrDefault(u => u.ProductId == productDto.ProductId);

            if (existingProduct == null)
            {
                return NotFound(new ResponseDto<ProductDto>
                {
                    IsSuccess = false,
                    Message = "Coupon not found"
                });
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.ImageUrl = productDto.ImageUrl;
            existingProduct.CategoryName = productDto.CategoryName;


            _db.Products.Update(existingProduct);
            _db.SaveChanges();

            var updatedProductDto = new ProductDto
            {
                ProductId = existingProduct.ProductId,
                Name = existingProduct.Name,
                Description = existingProduct.Description,
                Price = existingProduct.Price,
                ImageUrl = existingProduct.ImageUrl,
               
            };

            return Ok(new ResponseDto<ProductDto>
            {
                Result = updatedProductDto,
                IsSuccess = true,
                Message = "Product updated successfully"
            });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<ResponseDto<bool>> Delete(int id)
        {
            var product = _db.Products.FirstOrDefault(u => u.ProductId == id);

            if (product == null)
            {
                return NotFound(new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Product not found"
                });
            }

            _db.Products.Remove(product);
            _db.SaveChanges();

            return Ok(new ResponseDto<bool>
            {
                Result = true,
                IsSuccess = true,
                Message = "Product deleted successfully"
            });
        }
    }

}
