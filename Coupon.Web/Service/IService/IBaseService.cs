using Commerce.Web.Models;
using Coupon.Web.Models;






namespace Coupon.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto<T>> SendAsync<T>(RequestDto requestDto);
    }
}
