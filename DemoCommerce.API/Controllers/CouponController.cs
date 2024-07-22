using DemoCommerce.API.Data;
using DemoCommerce.API.Entity;
using DemoCommerce.API.Interface;
using DemoCommerce.API.Models.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoupons()
        {
            var coupons = await _couponService.GetAllCoupons();
            return Ok(coupons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            var coupon = await _couponService.GetCouponById(id);
            if (coupon == null) return NotFound();
            return Ok(coupon);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon([FromBody] CouponDto couponDto)
        {
            var createdCoupon = await _couponService.CreateCoupon(couponDto);
            return CreatedAtAction(nameof(GetCouponById), new { id = createdCoupon.CouponId }, createdCoupon);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoupon(int id, [FromBody] CouponDto couponDto)
        {
            var updatedCoupon = await _couponService.UpdateCoupon(id, couponDto);
            if (updatedCoupon == null) return NotFound();
            return Ok(updatedCoupon);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            await _couponService.DeleteCoupon(id);
            return NoContent();
        }
    }
}
