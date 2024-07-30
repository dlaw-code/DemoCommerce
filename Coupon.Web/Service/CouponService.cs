using Commerce.Web.Models;
using Coupon.Web.Service.IService;
using Coupon.Web.Utility;
using static Coupon.Web.Utility.SD;
using System;
using Coupon.Web.Models;

namespace Coupon.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto<CouponDto>?> CreateCouponsAsync(CouponDto couponDto)
        {
            var response = await _baseService.SendAsync<ResponseDto<CouponDto>>(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon" 
            });

             return response?.Result;
        }

        public async Task<ResponseDto<bool>?> DeleteCouponsAsync(int id)
        {
            var response = await _baseService.SendAsync<ResponseDto<bool>>(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/coupon/" + id
            });

            return response?.Result;
        }

        public async Task<ResponseDto<IEnumerable<CouponDto>>> GetAllCouponsAsync()
        {
            // Call SendAsync to get the response directly
            var response = await _baseService.SendAsync<ResponseDto<IEnumerable<CouponDto>>>(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon"
            });

            if (response == null || !response.IsSuccess)
            {
                // Handle the case where response is null or indicates failure
                return new ResponseDto<IEnumerable<CouponDto>>
                {
                    IsSuccess = false,
                    Message = response?.Message ?? "Failed to retrieve coupons"
                };
            }

            // Return the response
            return response?.Result;
        }


        public async Task<ResponseDto<CouponDto>?> GetCouponAsync(string couponCode)
        {
            var response = await _baseService.SendAsync<ResponseDto<CouponDto>>(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
            });

            return response?.Result;
        }

        public async Task<ResponseDto<CouponDto>?> GetCouponByIdAsync(int id)
        {
            var response = await _baseService.SendAsync<ResponseDto<CouponDto>>(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + id
            });

            return response?.Result;
        }

        public async Task<ResponseDto<CouponDto>?> UpdateCouponsAsync(CouponDto couponDto)
        {
            var response = await _baseService.SendAsync<ResponseDto<CouponDto>>(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon"
            });

            return response?.Result;
        }
    }


}
