namespace DemoCommerce.Web.Models
{
    public class CouponDto
    {
       
        public int CouponId { get; set; }
        public double DiscountAmount { get; set; }
        public required string CouponCode { get; set; }
        public double MinAmount { get; set; }   
    }
}


