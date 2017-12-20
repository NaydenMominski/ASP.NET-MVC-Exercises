
namespace CarDealer.Services.Implementations
{
    using Data;
    using System.Collections.Generic;
    using Models;
    using System.Linq;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierModel> All(bool IsImporter)
        => this.db
            .Suppliers
            .Where(s => s.IsImporter == IsImporter)
            .Select(s => new SupplierModel
            {
                Id=s.Id,
                Name = s.Name,
                TotalParts = s.Parts.Count
            })
            .ToList();
    }
}
