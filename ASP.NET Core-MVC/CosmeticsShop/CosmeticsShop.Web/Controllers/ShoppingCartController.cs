namespace CosmeticsShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CosmeticsShop.Data;
    using CosmeticsShop.Data.Models;
    using CosmeticsShop.Service;
    using CosmeticsShop.Service.Models.ShoppingCart;
    using CosmeticsShop.Web.Models.ShoppingCart;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
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
        [Authorize]
        public  IActionResult FinishOrder()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppinCartId();

            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            if (items==null)
            {
                return NotFound();
            }

            var itemIds = items.Select(i => i.ProductId);

            var itemsWithDetails = this.GetCartItems(items);

            var order = new Order
            {
                UserId = this.userManager.GetUserId(User),
                TotalPrice = itemsWithDetails.Sum(i => i.Price * i.Quantity)
            };

            foreach (var item in itemsWithDetails)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId=item.ProductId,
                    ProductPrice=item.Price,
                    ProductName=item.Title,
                    Quantity=item.Quantity
                });
            }

            this.db.Add(order);
            this.db.SaveChanges();

            shoppingCartManager.Clear(shoppingCartId);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private List<CartItemViewModel> GetCartItems(IEnumerable<CartItem> items)
        {
            var itemsIds = items.Select(i => i.ProductId);

            var itemsWithDetails = this.db
              .Products
              .Where(pr => itemsIds.Contains(pr.Id))
              .Select(pr => new CartItemViewModel
              {
                  ProductId = pr.Id,
                  Price = pr.Price,
                  Title = pr.Name
              })
              .ToList();

            var itemQuantities = items.ToDictionary(i => i.ProductId, i => i.Quantity);

            itemsWithDetails.ForEach(i => i.Quantity = itemQuantities[i.ProductId]);

            return itemsWithDetails;
        }
    }
}
