
namespace CarDealer.Services.Implementations
{
    using Data;
    using System.Collections.Generic;
    using Models.Suppliers;
    using System.Linq;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierModel> All()
            => this.db
                .Suppliers
                .OrderBy(s => s.Name)
                .Select(s => new SupplierModel
                {
                    Id=s.Id,
                    Name=s.Name
                })
            .ToList();

        public IEnumerable<SupplierListingModel> AllListing(bool IsImporter)
        => this.db
            .Suppliers
            .OrderByDescending(s => s.Id)
            .Where(s => s.IsImporter == IsImporter)
            .Select(s => new SupplierListingModel
            {
                Id=s.Id,
                Name = s.Name,
                TotalParts = s.Parts.Count
            })
            .ToList();
    }
}
