namespace CosmeticsShop.Service.Models.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCartServiceModel
    {
        private readonly IList<CartItem> items;

        public ShoppingCartServiceModel()
        {
            this.items = new List<CartItem>();
        }

        public IEnumerable<CartItem> Items => new List<CartItem>(this.items);

        public void AddToCart (Guid productId,int quantity)
        {
            var cartItem = this.items.FirstOrDefault(i => i.ProductId==productId);

            if (cartItem==null)
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                this.items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity = cartItem.Quantity + quantity;
            }
            
        }

        public void UpdateCartItem(Guid productId, int quantity)
        {
            var cartItem = this.items.FirstOrDefault(i => i.ProductId == productId);
            
                cartItem.Quantity =quantity;           
        }

        public void RemoveFromCart(Guid productId)
        {
            var cartItem = this.items
                .FirstOrDefault(i => i.ProductId == productId);

            if (cartItem!=null)
            {
                this.items.Remove(cartItem);
            }


        }

        public void Clear()=> this.items.Clear();



    }
}
