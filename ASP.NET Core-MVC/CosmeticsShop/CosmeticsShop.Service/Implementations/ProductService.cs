namespace CosmeticsShop.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore.Extensions.Internal;
    using Service;
    using Service.Models.Product;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {

        private readonly CosmeticsShopDbContext db;

        public ProductService(CosmeticsShopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<ProductsListingModel> AllListings(int page = 1, int pageSize = 10)
        {
            var all=this.db
              .Products
              .OrderByDescending(p => p.DateAdded)
              .Skip((page - 1) * pageSize)
              .Take(pageSize)
              .ProjectTo<ProductsListingModel>()
              .ToList();

            return all;
        }

        public void Create(
            string name,
            string description,
            decimal price,
            decimal? specialPrice,
            DateTime? specialPriceStartDate,
            DateTime? specialPriceEndDate,
            int stockQuantity,
            bool published,
            Guid categoryId,
            Guid manifacturerId,
            IEnumerable<string> imagesPaths)
        {

            var product = new Product
            {
                Name = name,
                Description=description,
                Price=price,
                SpecialPrice=specialPrice,
                SpecialPriceStartDate=specialPriceStartDate,
                SpecialPriceEndDate=specialPriceEndDate,
                StockQuantity=stockQuantity,
                Published=published,
                DateAdded=DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                CategoryId =categoryId,                
                ManifacturerId=manifacturerId,
                Images = imagesPaths
                .Select(p => new Image
                        {
                            ImageUrl = p,
                        })
                .ToList()
            };

            this.db.Add(product);
            this.db.SaveChanges();
        }

        public ProductServiceModel ById(Guid id)
            => this.db
            .Products
            .Where(c => c.Id == id)
            .ProjectTo<ProductServiceModel>()
            .FirstOrDefault();


        public TModel ById<TModel>(Guid id) where TModel : class
            => this.db
                .Products
                .Where(c => c.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefault();


        public void Delete(Guid id)
        {
            var product = this.db.Products.Find(id);

            if (product == null)
            {
                return;
            }

            this.db.Products.Remove(product);
            this.db.SaveChanges();

        }

        public int Total() => this.db.Products.Count();


        public void Edit(
            Guid id,
            string name,
            string description,
            decimal price,
            decimal? specialPrice,
            DateTime? specialPriceStartDate,
            DateTime? specialPriceEndDate,
            int stockQuantity,
            bool published,
            Guid CategoryId,
            Guid ManifacturerId,
            IEnumerable<string> imagesPaths)
        {
            var product = this.db.Products.Find(id);

            if (product == null)
            {
                return;
            }

            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.SpecialPrice = specialPrice;
            product.SpecialPriceStartDate = specialPriceStartDate;
            product.SpecialPriceEndDate = specialPriceEndDate;
            product.StockQuantity = stockQuantity;
            product.Published = published;
            product.DateModified = DateTime.UtcNow;
            product.CategoryId = CategoryId;
            product.ManifacturerId = ManifacturerId;

            if (imagesPaths.Count()!=0)
            {
                product.Images = imagesPaths
                    .Select(i => new Image
                    {
                        ImageUrl = i,
                    })
                    .ToList();
            };
            
            this.db.SaveChanges();
            
        }


        public bool Exists(Guid id)
            => this.db.Products.Any(c => c.Id == id);


        public ProductWithImagesServiceModel Details(Guid id)
        {
            var productDetails = this.db
              .Products
              .Where(b => b.Id == id)
              .ProjectTo<ProductWithImagesServiceModel>()
              .FirstOrDefault();

            return productDetails;
        }

        public List<TModel> GetListProductWithDetails<TModel>(IEnumerable<Guid> itemsIds) where TModel : class
            => this.db
                  .Products
                  .Where(pr => itemsIds.Contains(pr.Id))
                  .ProjectTo<TModel>()
                  .ToList();




    }
}
