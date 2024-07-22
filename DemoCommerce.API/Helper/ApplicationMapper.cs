using AutoMapper;
using DemoCommerce.API.Entity;
using DemoCommerce.API.Models.ResponseDto;

namespace DemoCommerce.API.Helper
{
    public class ApplicationMapper: Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Coupon, CouponDto>()
            .ReverseMap();
        }
    }
}
