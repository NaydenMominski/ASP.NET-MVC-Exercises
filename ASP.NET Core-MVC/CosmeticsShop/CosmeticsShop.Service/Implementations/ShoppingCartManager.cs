﻿

namespace CosmeticsShop.Service.Implementations
{
    using Models.ShoppingCart;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly ConcurrentDictionary<string, ShoppingCartServiceModel> cards;

        public ShoppingCartManager()
        {
            this.cards = new ConcurrentDictionary<string, ShoppingCartServiceModel>();
        }


        public void AddToCart(string id, Guid productId,int quantity)
        {
            var shoppingCart = this.GetShoppingCart(id);

            shoppingCart.AddToCart(productId, quantity);
        }
        
        public void UpdateCartItem(string id, Guid productId, int quantity)
        {
            var shoppingCart = this.GetShoppingCart(id);

            shoppingCart.UpdateCartItem(productId, quantity);
        }

        public void RemoveFromCart(string id, Guid productId)
        {
            var shoppingCart = this.GetShoppingCart(id);

            shoppingCart.RemoveFromCart(productId);
        }

        public IEnumerable<CartItem> GetItems(string id)
        {
            var shoppingCart = this.GetShoppingCart(id);

            return new List<CartItem>(shoppingCart.Items);
        }

        public void Clear(string id) => this.GetShoppingCart(id).Clear();
       

        //public IEnumerable<ShoppingCartItem> itemsWithDetails(IEnumerable<Guid> itemIds)
        //    =>this.db
        //       .Products
        //       .Where(pr => itemIds.Contains(pr.Id))
        //       .ProjectTo<ShoppingCartItem>()
        //       .ToList();


        private ShoppingCartServiceModel GetShoppingCart(string id)
            => this.cards.GetOrAdd(id, new ShoppingCartServiceModel());


    }
}
