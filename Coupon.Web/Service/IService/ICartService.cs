using Commerce.Web.Models;


namespace Coupon.Web.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDto?> GetCartByUserIdAsync(string userId);
        Task<ResponseDto?> UpsertCartAsync(CartDto cardDto);
        Task<ResponseDto?> RemoveFromCartAsync(int cardDetailsId);
        Task<ResponseDto?>ApplyCouponAsync(CartDto cardDto);
       
       


    }
}
