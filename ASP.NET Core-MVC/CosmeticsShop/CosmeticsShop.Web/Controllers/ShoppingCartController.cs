namespace CosmeticsShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CosmeticsShop.Data;
    using CosmeticsShop.Data.Models;
    using CosmeticsShop.Service;
    using CosmeticsShop.Service.Models.ShoppingCart;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartController: Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly IProductService product;
        private readonly CosmeticsShopDbContext db;
        private readonly UserManager<User> userManager;

        public ShoppingCartController(IShoppingCartManager shoppingCartManager, IProductService product, CosmeticsShopDbContext db, UserManager<User> userManager)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.product = product;
            this.db = db;
            this.userManager = userManager;
        }

        public IActionResult Items()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppinCartId();
            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            var itemIds = items.Select(i => i.ProductId);

            var itemsWithDetails = this.GetCartItems(items);

            return View(itemsWithDetails);
        }

        public IActionResult AddToCart(Guid id,int qty=1)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppinCartId();

            this.shoppingCartManager.AddToCart(shoppingCartId, id, qty);
            
            return RedirectToAction(nameof(Items));
           
        }
        public IActionResult RemoveFromCart(Guid id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppinCartId();

            this.shoppingCartManager.RemoveFromCart(shoppingCartId, id);

            return RedirectToAction(nameof(Items));

        }

        public IActionResult UpdateCartItem(Guid id,int qty)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppinCartId();

            this.shoppingCartManager.UpdateCartItem(shoppingCartId, id, qty);

            return RedirectToAction(nameof(Items));
        }

        [Authorize]
        public  IActionResult FinishOrder()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppinCartId();

            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            if (items==null)
            {
                return NotFound();
            }

            var itemIds = items.Select(i => i.ProductId).ToList();

            var itemsWithDetails = this.GetCartItems(items);

            var order = new Order
            {
                UserId = this.userManager.GetUserId(User),
                TotalPrice = itemsWithDetails.Sum(i => i.CurentPrice)
            };

            foreach (var item in itemsWithDetails)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId=item.Id,
                    ProductPrice= item.CurentPrice,
                    ProductName =item.Name,
                    Quantity=item.Quantity
                });
            }

            this.db.Add(order);
            this.db.SaveChanges();

            shoppingCartManager.Clear(shoppingCartId);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize]
        public IActionResult BillingAddress()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppinCartId();
            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            var itemIds = items.Select(i => i.ProductId);

            var itemsWithDetails = this.GetCartItems(items);

            return View(itemsWithDetails);
        }

        private List<ShoppingCartItem> GetCartItems(IEnumerable<CartItem> items)
        {
            var itemsIds = items.Select(i => i.ProductId);

            var itemsWithDetails = product.GetListProductWithDetails<ShoppingCartItem>(itemsIds);
            
            var itemQuantities = items.ToDictionary(i => i.ProductId, i => i.Quantity);

            itemsWithDetails.ForEach(i => i.Quantity = itemQuantities[i.Id]);
            itemsWithDetails.ForEach(i =>i.CurentPrice=(i.IsOnSalle)?i.SpecialPrice:i.Price);
            return itemsWithDetails;
        }
    }
}
