using Commerce.Web.Models;


namespace Coupon.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto<CouponDto>?> GetCouponAsync(string couponCode);
        Task<ResponseDto<IEnumerable<CouponDto>>?> GetAllCouponsAsync();
        Task<ResponseDto<CouponDto>?> GetCouponByIdAsync(int id);
        Task<ResponseDto<CouponDto>?> CreateCouponsAsync(CouponDto couponDto);
        Task<ResponseDto<CouponDto>?> UpdateCouponsAsync(CouponDto couponDto);
        Task<ResponseDto<bool>?> DeleteCouponsAsync(int id);
       


    }
}
