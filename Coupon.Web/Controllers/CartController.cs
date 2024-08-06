using Commerce.Web.Models;
using Coupon.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;

namespace Commerce.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        public async Task <IActionResult> CartIndex()
        {

            return View(await LoadCartDtoBasedOnLoggedinUser());
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.RemoveFromCartAsync(cartDetailsId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart Updated Successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            else
            {
                return View(); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            
            ResponseDto? response = await _cartService.ApplyCouponAsync(cartDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart Updated Successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {

            cartDto.CartHeader.CouponCode = "";
            ResponseDto? response = await _cartService.ApplyCouponAsync(cartDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart Updated Successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            else
            {
                return View();
            }
        }


        private async Task<CartDto> LoadCartDtoBasedOnLoggedinUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.GetCartByUserIdAsync(userId);

            if (response != null && response.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }
            else
            {
                return new CartDto(); // Return an empty CartDto or handle accordingly
            }
        }

    }
}
