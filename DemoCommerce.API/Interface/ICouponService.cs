﻿using DemoCommerce.API.Models.ResponseDto;

namespace DemoCommerce.API.Interface
{
    public interface ICouponService
    {
        Task<IEnumerable<CouponDto>> GetAllCoupons();
        Task<CouponDto> GetCouponById(int id);
        Task<CouponDto> CreateCoupon(CouponDto couponDto);
        Task<CouponDto> UpdateCoupon(int id, CouponDto couponDto);
        Task DeleteCoupon(int id);
    }
}
