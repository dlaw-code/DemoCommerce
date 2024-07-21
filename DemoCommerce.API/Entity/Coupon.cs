namespace DemoCommerce.API.Entity
{
    public class Coupon
    {
        public int Id { get; set; }   
        public int CouponId { get; set; }       
        public required  string CouponCode { get; set; }
        public double MinAmount { get; set; }                                                             
    }
}
