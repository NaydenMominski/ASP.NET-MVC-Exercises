
namespace CosmeticsShop.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Manifacturer;
    using Service;

    public class ManifacturerController: Controller
    {
        private readonly IManifacturerService manifacturer;

        public ManifacturerController(IManifacturerService manifacturer)
        {
            this.manifacturer = manifacturer;
        }


        public IActionResult Create()
           => this.View();

        [HttpPost]
        public IActionResult Create(ManifacturerFormViewModel manifacturerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(manifacturerModel);
            }

            this.manifacturer.Create(
                manifacturerModel.Name,
                manifacturerModel.Description,
                manifacturerModel.MainImageUrl,
                manifacturerModel.Published
                );

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }
    }
}
