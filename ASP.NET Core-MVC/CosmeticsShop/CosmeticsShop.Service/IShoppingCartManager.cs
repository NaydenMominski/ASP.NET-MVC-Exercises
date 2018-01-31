
namespace CosmeticsShop.Service
{
    using Models.ShoppingCart;
    using System;
    using System.Collections.Generic;

    public interface IShoppingCartManager
    {
        void AddToCart(string id, Guid productId,int quantity);

        //void UpdateQuantity(string id, CartItem cartItem);

        void RemoveFromCart(string id, Guid productId );

        IEnumerable<CartItem> GetItems(string id);

        //IEnumerable<ShoppingCartItem> itemsWithDetails(IEnumerable<Guid> itemIds);

        void Clear(string Id);

    }
}
