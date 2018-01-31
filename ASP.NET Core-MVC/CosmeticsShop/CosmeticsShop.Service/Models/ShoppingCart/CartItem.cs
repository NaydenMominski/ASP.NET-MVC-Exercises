

namespace CosmeticsShop.Service.Models.ShoppingCart
{
    using System;

    public class CartItem
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}