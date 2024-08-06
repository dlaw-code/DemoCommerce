﻿namespace Commerce.Web.Models
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; } = new CartHeaderDto();
        public IEnumerable<CartDetailsDto>? CartDetails { get; set; } = new List<CartDetailsDto>();
    }

}
