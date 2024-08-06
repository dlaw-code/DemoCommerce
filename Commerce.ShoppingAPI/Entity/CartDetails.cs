﻿using Commerce.ShoppingAPI.Models.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Commerce.ShoppingAPI.Entity
{
    public class CartDetails
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}
