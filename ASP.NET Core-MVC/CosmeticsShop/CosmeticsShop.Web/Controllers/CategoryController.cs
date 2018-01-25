
namespace CosmeticsShop.Web.Controllers
{
    using CosmeticsShop.Service;
    using CosmeticsShop.Web.Models.Category;
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController :Controller

    {
        private readonly ICategoryService category;

        public CategoryController(ICategoryService category)
        {
            this.category = category;
        }


        public IActionResult Create()
           => this.View();

        [HttpPost]
        public IActionResult Create(CategoryFormViewModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryModel);
            }

            this.category.Create(
                categoryModel.Name,
                categoryModel.Description,
                categoryModel.Published
                );

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }
    }
}



