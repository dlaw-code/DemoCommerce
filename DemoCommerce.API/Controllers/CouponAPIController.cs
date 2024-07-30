using AutoMapper;
using DemoCommerce.API.Data;
using DemoCommerce.API.Entity;
using DemoCommerce.API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using DemoCommerce.API.Models.ResponseDto;
using Microsoft.EntityFrameworkCore;

namespace DemoCommerce.API.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<ResponseDto<IEnumerable<CouponDto>>> Get()
        {
            var coupons = _db.Coupons
                .Select(c => new CouponDto
                {
                    CouponId = c.CouponId,
                    CouponCode = c.CouponCode,
                    DiscountAmount = c.DiscountAmount,
                    MinAmount = c.MinAmount
                })
                .ToList();

            return Ok(new ResponseDto<IEnumerable<CouponDto>>
            {
                Result = coupons,
                IsSuccess = true,
                Message = "Coupons retrieved successfully"
            });
        }

        [HttpGet("{id:int}")]
        public ActionResult<ResponseDto<CouponDto>> GetById(int id)
        {
            var coupon = _db.Coupons
                .Where(c => c.CouponId == id)
                .Select(c => new CouponDto
                {
                    CouponId = c.CouponId,
                    CouponCode = c.CouponCode,
                    DiscountAmount = c.DiscountAmount,
                    MinAmount = c.MinAmount
                })
                .FirstOrDefault();

            if (coupon == null)
            {
                return NotFound(new ResponseDto<CouponDto>
                {
                    IsSuccess = false,
                    Message = "Coupon not found"
                });
            }

            return Ok(new ResponseDto<CouponDto>
            {
                Result = coupon,
                IsSuccess = true,
                Message = "Coupon retrieved successfully"
            });
        }

        [HttpPost]
        public ActionResult<ResponseDto<CouponDto>> Post([FromBody] CouponDto couponDto)
        {
            var coupon = new Coupon
            {
                CouponCode = couponDto.CouponCode,
                DiscountAmount = couponDto.DiscountAmount,
                MinAmount = couponDto.MinAmount
            };

            _db.Coupons.Add(coupon);
            _db.SaveChanges();

            couponDto.CouponId = coupon.CouponId;

            return CreatedAtAction(nameof(GetById), new { id = coupon.CouponId }, new ResponseDto<CouponDto>
            {
                Result = couponDto,
                IsSuccess = true,
                Message = "Coupon created successfully"
            });
        }

        [HttpGet("GetByCode/{code}")]
        public async Task<ActionResult<ResponseDto<CouponDto>>> GetByCode(string code)
        {
            var coupon = await _db.Coupons
                .Where(u => u.CouponCode.ToLower() == code.ToLower())
                .Select(c => new CouponDto
                {
                    CouponId = c.CouponId,
                    CouponCode = c.CouponCode,
                    DiscountAmount = c.DiscountAmount,
                    MinAmount = c.MinAmount
                })
                .FirstOrDefaultAsync();

            if (coupon == null)
            {
                return NotFound(new ResponseDto<CouponDto>
                {
                    IsSuccess = false,
                    Message = "Coupon not found"
                });
            }

            return Ok(new ResponseDto<CouponDto>
            {
                Result = coupon,
                IsSuccess = true,
                Message = "Coupon retrieved successfully"
            });
        }

        [HttpPut]
        public ActionResult<ResponseDto<CouponDto>> Put([FromBody] CouponDto couponDto)
        {
            var existingCoupon = _db.Coupons.FirstOrDefault(u => u.CouponId == couponDto.CouponId);

            if (existingCoupon == null)
            {
                return NotFound(new ResponseDto<CouponDto>
                {
                    IsSuccess = false,
                    Message = "Coupon not found"
                });
            }

            existingCoupon.CouponCode = couponDto.CouponCode;
            existingCoupon.DiscountAmount = couponDto.DiscountAmount;
            existingCoupon.MinAmount = couponDto.MinAmount;

            _db.Coupons.Update(existingCoupon);
            _db.SaveChanges();

            var updatedCouponDto = new CouponDto
            {
                CouponId = existingCoupon.CouponId,
                CouponCode = existingCoupon.CouponCode,
                DiscountAmount = existingCoupon.DiscountAmount,
                MinAmount = existingCoupon.MinAmount
            };

            return Ok(new ResponseDto<CouponDto>
            {
                Result = updatedCouponDto,
                IsSuccess = true,
                Message = "Coupon updated successfully"
            });
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ResponseDto<bool>> Delete(int id)
        {
            var coupon = _db.Coupons.FirstOrDefault(u => u.CouponId == id);

            if (coupon == null)
            {
                return NotFound(new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Coupon not found"
                });
            }

            _db.Coupons.Remove(coupon);
            _db.SaveChanges();

            return Ok(new ResponseDto<bool>
            {
                Result = true,
                IsSuccess = true,
                Message = "Coupon deleted successfully"
            });
        }
    }

}
