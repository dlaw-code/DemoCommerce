using System.ComponentModel.DataAnnotations.Schema;

namespace Commerce.ShoppingAPI.Entity
{
    public class CartHeader
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string? CouponCode { get; set; }
        [NotMapped]
        public double Discount { get; set; }
        [NotMapped]
        public double CartTotal { get; set; }
    }
}
