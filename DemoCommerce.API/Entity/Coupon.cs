﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DemoCommerce.API.Entity
{
    public class Coupon
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public required string CouponCode { get; set; }
        public double MinAmount { get; set; }


        public class CouponConfig : IEntityTypeConfiguration<Coupon>
        {
            public void Configure(EntityTypeBuilder<Coupon> builder)
            {
                //Configurations goes here
            }
        }
    }
}
