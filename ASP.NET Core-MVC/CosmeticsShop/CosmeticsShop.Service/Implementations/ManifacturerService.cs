namespace CosmeticsShop.Service.Implementations
{
    using Data;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Models.Manifacturer;
    using AutoMapper.QueryableExtensions;

    class ManifacturerService : IManifacturerService
    {
        private readonly CosmeticsShopDbContext db;

        public ManifacturerService(CosmeticsShopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<ManifacturerBasicModel> All()
            => this.db
            .Manufacturers
            .OrderBy(c => c.Name)
            .ProjectTo<ManifacturerBasicModel>()
            .ToList();

        public void Create(string name, string description, string mainImageUrl, bool published)
        {
            var manifacturer = new Manifacturer
            {
                Name = name,
                Description = description,
                MainImageUrl = mainImageUrl,
                Published = published,
                DateAdded = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            this.db.Add(manifacturer);
            this.db.SaveChanges();
        }
    }
}
