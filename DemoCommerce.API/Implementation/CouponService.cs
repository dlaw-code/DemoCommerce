using AutoMapper;
using DemoCommerce.API.Data;
using DemoCommerce.API.Entity;
using DemoCommerce.API.Interface;
using DemoCommerce.API.Models.ResponseDto;
using Microsoft.EntityFrameworkCore;

namespace DemoCommerce.API.Implementation
{
    public class CouponService : ICouponService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CouponService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CouponDto>> GetAllCoupons()
        {
            var coupons = await _context.Coupons.ToListAsync();
            return _mapper.Map<IEnumerable<CouponDto>>(coupons);
        }

        public async Task<CouponDto> GetCouponById(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null) return null;
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<CouponDto> CreateCoupon(CouponDto couponDto)
        {
            var coupon = _mapper.Map<Coupon>(couponDto);
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<CouponDto> UpdateCoupon(int id, CouponDto couponDto)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null) return null;

            _mapper.Map(couponDto, coupon);
            _context.Coupons.Update(coupon);
            await _context.SaveChangesAsync();

            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task DeleteCoupon(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null) return;

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
        }
    }

}
