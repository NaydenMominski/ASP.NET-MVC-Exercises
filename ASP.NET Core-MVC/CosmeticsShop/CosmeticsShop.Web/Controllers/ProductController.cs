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
    using Microsoft.AspNetCore.Hosting;

    public class ProductController:Controller
    {
        private const int PageSize = 10;
        string uploadsFolderPath = "pictures";

        private readonly IProductService product;
        private readonly ICategoryService category;
        private readonly IManifacturerService manifacturer;
        private readonly IHostingEnvironment env;

        public ProductController( IProductService product, ICategoryService category, IManifacturerService manifacturer, IHostingEnvironment env)
        {
            this.product = product;
            this.category = category;
            this.manifacturer = manifacturer;
            this.env = env;
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
            //var productPicturesPath = this.GetAdequateProductPicturesPath();

            foreach (var imageFile in productModel.Images)
            {
                if (imageFile.Length > 0)
                {
                    
                    if (!Directory.Exists(uploadsFolderPath))
                    {
                        Directory.CreateDirectory(uploadsFolderPath);
                    }

                    var imageFullPath = Path.Combine(this.env.WebRootPath, uploadsFolderPath, Path.GetFileName(imageFile.FileName));

                    using (var stream = new FileStream(imageFullPath, FileMode.Create))
                    {
                        Task.Run(async () =>
                        {
                            await imageFile.CopyToAsync(stream);
                        }).Wait();

                        var relativePicturePath = "/" + uploadsFolderPath + "/" + Path.GetFileName(imageFile.FileName);

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
            var Products = this.product.AllListings(page, PageSize);


            var t = this.product.Total();
            return View(new ProductPageListingModel
            {
                Products = Products,
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
        public IActionResult Details(Guid id)
        {
            var product = this.product.Details(id);

            if (product == null)
            {
                return NotFound();
            }
 
            var details= new ProductDetailsViewModel
            {
                Name = product.Name,
                Id = product.Id,
                Images = product.Images
            };

            return View(details);
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

                foreach (var imageFile in productModel.Images)
                {
                    if (imageFile.Length > 0)
                    {
                        
                        if (!Directory.Exists(uploadsFolderPath))
                        {
                            Directory.CreateDirectory(uploadsFolderPath);
                        }

                        var imageFullPath = Path.Combine(this.env.WebRootPath, uploadsFolderPath, Path.GetFileName(imageFile.FileName));


                        using (var stream = new FileStream(imageFullPath, FileMode.Create))
                        {
                            Task.Run(async () =>
                            {
                                await imageFile.CopyToAsync(stream);
                            }).Wait();

                            var relativePicturePath = "/" + uploadsFolderPath + "/" + Path.GetFileName(imageFile.FileName);

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
            var p = product.ById(id);

            foreach (var image in product.ById(id).Images)
            {
                var fullPath = this.env.WebRootPath + image.ImageUrl;
                if (System.IO.File.Exists(fullPath))
                {
                     System.IO.File.Delete(fullPath);                   
                }
            }
                              
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
