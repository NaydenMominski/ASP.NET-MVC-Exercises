

namespace CosmeticsShop.Service.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Models.Category;
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly CosmeticsShopDbContext db;

        public CategoryService(CosmeticsShopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CategoryBasicModel> All()
            => this.db
            .Categories
            .OrderBy(c=>c.Name)
            .ProjectTo<CategoryBasicModel>()
            .ToList();


        public  TModel ById<TModel>(Guid id) where TModel : class
            => this.db
                .Categories
                .Where(c => c.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefault();

        public void Create(string name, string description, bool published)
        {
            var category = new Category
            {
                Name = name,
                Description = description,
                Published = published,
                DateAdded = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            this.db.Add(category);
            this.db.SaveChanges();
        }
    }
}
