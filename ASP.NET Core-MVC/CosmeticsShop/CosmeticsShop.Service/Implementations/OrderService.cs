namespace CosmeticsShop.Service.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data;
    using Data.Models;
    using System.Linq;


    public class OrderService : IOrderService
    {
        private readonly CosmeticsShopDbContext db;

        public OrderService(CosmeticsShopDbContext db)
        {
            this.db = db;
        }

        public void Create(string userId, decimal totalPrice, IEnumerable<OrderItem> orderItems)
        {
            throw new NotImplementedException();
        }

        //public void Create(string userId, decimal totalPrice, IEnumerable<> orderItems)
        //{
        //    var order = new Order
        //        {
        //            UserId = userId,
        //            TotalPrice = totalPrice,
        //            Items= OrderItem
        //            .Select(p => new OrderItem
        //            {
        //                ProductId = item.ProductId,
        //                ProductPrice = item.ProductPrice,
        //                ProductName = item.ProductName,
        //                Quantity = item.Quantity
        //            })
        //    };

        //        foreach (var item in orderItems)
        //        {
        //            order.Items.Add(new OrderItem
        //            {
        //                ProductId = item.ProductId,
        //                ProductPrice = item.ProductPrice,
        //                ProductName = item.ProductName,
        //                Quantity = item.Quantity
        //            });
        //        }

        //        this.db.Add(order);
        //        this.db.SaveChanges();
        //    }
    }
}
