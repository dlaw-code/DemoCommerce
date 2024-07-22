using AutoMapper;
using DemoCommerce.API.Entity;
using DemoCommerce.API.Models.ResponseDto;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoCommerce.API
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
