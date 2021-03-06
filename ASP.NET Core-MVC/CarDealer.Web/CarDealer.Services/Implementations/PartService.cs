﻿

namespace CarDealer.Services.Implementations
{
    using Data;
    using Data.Models;
    using System.Collections.Generic;
    using Models.Parts;
    using System.Linq;

    public class PartService : IPartService
    {
        
        private readonly CarDealerDbContext db;

        public PartService (CarDealerDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, decimal price, int quantity, int supplierId)
        {
            var part = new Part
            {
                Name = name,
                Price = price,
                Quantity = quantity > 0 ? quantity : 1,
                SupplierId = supplierId
            };
            this.db.Add(part);
            this.db.SaveChanges();
        }

        public void Edit(int id, decimal price, int quantity)
        {
            var part = this.db.Parts.Find(id);

            if (part==null)
            {
                return;
            }

            part.Price = price;
            part.Quantity = quantity;

            this.db.SaveChanges();
        }

        public IEnumerable<PartListingModel> AllListings(int page = 1,int pageSize = 10)
            => this.db
            .Parts
            .OrderByDescending(p=>p.Id)
            .Skip((page-1)* pageSize)
            .Take(pageSize)
            .Select(p => new PartListingModel
            {
                Id=p.Id,
                Name=p.Name,
                Price=p.Price,
                Quantity=p.Quantity,
                SupplierName=p.Supplier.Name
            })
            .ToList();

        public IEnumerable<PartBasicModel> All()
            => this.db
            .Parts
            .OrderBy(p=>p.Id)
            .Select(p => new PartBasicModel
            {
                Id = p.Id,
                Name = p.Name
            })
            .ToList();

        public PartDetailsModel ById(int id)
            => this.db
                .Parts
                .Where(p => p.Id == id)
                .Select(p => new PartDetailsModel
                {
                    Name=p.Name,
                    Price=p.Price,
                    Quantity=p.Quantity
                })
                .FirstOrDefault();

        public void Delete(int id)
        {
            var part = this.db.Parts.Find(id);

            if (part==null)
            {
                return;
            }

            this.db.Parts.Remove(part);
            this.db.SaveChanges();

        }

        public int Total() => this.db.Parts.Count();


    }
}
