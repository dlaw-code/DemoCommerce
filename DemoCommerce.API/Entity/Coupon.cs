using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DemoCommerce.API.Entity
{
    public class Coupon
    {
       
        public int CouponId { get; set; }
        public double DiscountAmount { get; set; }
        public required string CouponCode { get; set; }
        public int MinAmount { get; set; }


        //public class CouponConfig : IEntityTypeConfiguration<Coupon>
        //{
        //    public void Configure(EntityTypeBuilder<Coupon> builder)
        //    {
        //        //Configurations goes here
        //    }
        //}
    }
}
