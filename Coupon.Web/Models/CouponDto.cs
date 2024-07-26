﻿using System.ComponentModel.DataAnnotations;

namespace Commerce.Web.Models
{
    public class CouponDto
    {
        public int CouponId { get; set; }

        [Required]
        public string CouponCode { get; set; }

        public double DiscountAmount { get; set; }
        public double MinAmount { get; set; }
    }
}



