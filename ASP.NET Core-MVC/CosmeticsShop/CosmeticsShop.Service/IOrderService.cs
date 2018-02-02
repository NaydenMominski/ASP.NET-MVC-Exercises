
namespace CosmeticsShop.Service
{
    using Data.Models;
    using System.Collections.Generic;

    public interface IOrderService
    {
        void Create(string userId, decimal totalPrice, IEnumerable<OrderItem> orderItems);
    }
}
