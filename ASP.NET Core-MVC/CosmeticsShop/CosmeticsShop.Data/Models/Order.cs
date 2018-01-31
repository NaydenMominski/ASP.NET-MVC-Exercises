
namespace CosmeticsShop.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        //Address

        public decimal TotalPrice { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
