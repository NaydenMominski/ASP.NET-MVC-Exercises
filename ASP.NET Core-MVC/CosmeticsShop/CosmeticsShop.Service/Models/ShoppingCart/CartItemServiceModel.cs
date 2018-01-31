

namespace CosmeticsShop.Service.Models.ShoppingCart
{
    using System;

    public class CartItemServiceModel
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        //public decimal Price { get; set; }
    }
}
