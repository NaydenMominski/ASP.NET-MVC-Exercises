

namespace CosmeticsShop.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using CosmeticsShop.Web.Models;
    using CosmeticsShop.Service;
    using CosmeticsShop.Web.Models.Product;
    using System;

    public class HomeController : Controller
    {
        private const int PageSize = 10;

        private readonly IProductService product;

        public HomeController(IProductService product)
        {
            this.product = product;
        }

        public IActionResult Index(int page = 1)
        {
            var model = new ProductPageListingModel
            {
                Products = this.product.AllListings(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.product.Total() / (double)PageSize)
            };

            if (model.Products == null)
            {
                return NotFound();
            }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
