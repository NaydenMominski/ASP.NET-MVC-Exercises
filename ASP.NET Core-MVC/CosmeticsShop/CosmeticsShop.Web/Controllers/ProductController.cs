namespace CosmeticsShop.Web.Controllers
{
    using Service;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models.Product;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ProductController:Controller
    {
        private const int PageSize = 25;

        private readonly IImageService imageService;
        private readonly IProductService product;
        private readonly ICategoryService category;
        private readonly IManifacturerService manifacturer;

        public ProductController(IImageService imageService, IProductService product, ICategoryService category, IManifacturerService manifacturer)
        {
            this.imageService = imageService;
            this.product = product;
            this.category = category;
            this.manifacturer = manifacturer;
        }

        [HttpGet]
        public IActionResult Create()
            => View(new ProductFormViewModel
            {
                AllCategory = this.GetCategorySelectItems(),
                AllManifacturer = this.GetManifacturerSelectItems()
            });

        [HttpPost]
        public IActionResult Create(ProductFormViewModel productModel)
        {
            if (!ModelState.IsValid)
            {
                productModel.AllCategory = this.GetCategorySelectItems();
                productModel.AllManifacturer = this.GetManifacturerSelectItems();

                return View(productModel);
            }

            if (productModel.Images == null || productModel.Images.Count == 0)
            {
                productModel.AllCategory = this.GetCategorySelectItems();
                productModel.AllManifacturer = this.GetManifacturerSelectItems();
                this.ModelState.AddModelError("Pictures", "You need to add at least 1 picture of your home.");

                return this.View(productModel);
            }

            List<string> imagePaths = new List<string>();

            //save pictures
            var productPicturesPath = this.GetAdequateProductPicturesPath();

            foreach (var picture in productModel.Images)
            {
                if (picture.Length > 0)
                {
                    var imageFullPath = Path.Combine(productPicturesPath, picture.FileName);

                    using (var stream = new FileStream(imageFullPath, FileMode.Create))
                    {
                        Task.Run(async () =>
                        {
                            await picture.CopyToAsync(stream);
                        }).Wait();

                        var pathTokens = imageFullPath
                            .Split(new[] { "\\" }, StringSplitOptions.None);

                        var relativePicturePath = string.Join("/", pathTokens.Skip(pathTokens.Length - 2));

                        imagePaths.Add(relativePicturePath);
                    }
                }
            }
              this.product
                .Create(
                productModel.Name,
                productModel.Description,
                productModel.Price,
                productModel.SpecialPrice,                
                productModel.SpecialPriceStartDate,
                productModel.SpecialPriceEndDate,
                productModel.StockQuantity,
                productModel.Published,
                productModel.SelectedCategoryId,
                productModel.SelectedManifacturerId,
                imagePaths

                );

            return RedirectToAction("All", "Product");
        }
        
        public IActionResult All(int page = 1)
        {
            var Products1 = this.product.AllListings(page, PageSize);

            foreach (var p in Products1)
            {
                p.MaingImagePath= this.imageService.PreparePictureToDisplay(p.MaingImagePath);
            }

            var t = this.product.Total();
            return View(new ProductPageListingModel
            {
                Products = Products1,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.product.Total() / (double)PageSize)

            });
        }

        public IActionResult Delete(Guid id) 
            => View(id);

        public IActionResult Destroy(Guid id)
        {
            DeleteExistingFile(id);

            //delete product from Data
            this.product.Delete(id);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var product = this.product.ById(id);

            if (product == null)
            {
                return NotFound();
            }
            
            return View(new ProductFormViewModel
            {
                Name = product.Name,
                Description=product.Description,
                Price=product.Price,
                SpecialPrice=product.SpecialPrice,
                SpecialPriceStartDate=product.SpecialPriceStartDate,
                SpecialPriceEndDate=product.SpecialPriceEndDate,
                StockQuantity=product.StockQuantity,
                Published=product.Published,
                AllCategory= this.GetCategorySelectItems(),
                AllManifacturer=this.GetManifacturerSelectItems(),
                SelectedCategoryId=product.Category.Id,
                SelectedManifacturerId=product.Manifacturer.Id,               

            });

        }

        [HttpPost]
        public IActionResult Edit(Guid id, ProductFormViewModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productModel);
            }

            var productExists = this.product.Exists(id);

            if (!productExists)
            {
                return NotFound();
            }

            List<string> imagePaths = new List<string>();

            if (productModel.Images != null)
            {
                //delete existing pictures 

                DeleteExistingFile(id);

                //save pictures
                var productPicturesPath = this.GetAdequateProductPicturesPath();

                foreach (var picture in productModel.Images)
                {
                    if (picture.Length > 0)
                    {
                        var imageFullPath = Path.Combine(productPicturesPath, picture.FileName);

                        using (var stream = new FileStream(imageFullPath, FileMode.Create))
                        {
                            Task.Run(async () =>
                            {
                                await picture.CopyToAsync(stream);
                            }).Wait();

                            var pathTokens = imageFullPath
                                .Split(new[] { "\\" }, StringSplitOptions.None);

                            var relativePicturePath = string.Join("/", pathTokens.Skip(pathTokens.Length - 2));

                            imagePaths.Add(relativePicturePath);
                        }
                    }
                }
            }

            this.product.Edit(
                id,
                productModel.Name,
                productModel.Description,
                productModel.Price,
                productModel.SpecialPrice,
                productModel.SpecialPriceStartDate,
                productModel.SpecialPriceEndDate,
                productModel.StockQuantity,
                productModel.Published,
                productModel.SelectedCategoryId,
                productModel.SelectedManifacturerId,
                imagePaths
                );
            return RedirectToAction("All", "Product");
        }

        private void DeleteExistingFile(Guid id)
        {
            var fullPath = this.imageService.GetProductPicturesFullPath(id);

            //delete existing picture
            DirectoryInfo di = new DirectoryInfo(fullPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        private string GetAdequateProductPicturesPath()
        {
            var currentProductDirectory = Guid.NewGuid();

            string path = this.imageService.GetFilePath($"Images/ProductPictures/{currentProductDirectory}");

            Directory.CreateDirectory(path);

            return path;
        }

        private IEnumerable<SelectListItem> GetCategorySelectItems()
            => this.category
                .All()
                .Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                });

        private IEnumerable<SelectListItem> GetManifacturerSelectItems()
           => this.manifacturer
               .All()
               .Select(p => new SelectListItem
               {
                   Text = p.Name,
                   Value = p.Id.ToString()
               });
    }
}
