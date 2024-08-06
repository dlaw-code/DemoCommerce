using Commerce.ShoppingAPI.Models.Dto;
using Commerce.ShoppingAPI.Models.ResponseDto;

namespace Commerce.ShoppingAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
