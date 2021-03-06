﻿using System;

namespace CosmeticsShop.Data.Models
{
    public class OrderItem
    {
        public Guid  Id { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

    }
}
