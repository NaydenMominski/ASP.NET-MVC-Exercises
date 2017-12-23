namespace CarDealer.Services
{
    using System.Collections.Generic;
    using Models.Suppliers;

    public interface ISupplierService
    {
        IEnumerable<SupplierListingModel> AllListing(bool IsImporter);

        IEnumerable<SupplierModel> All();
    }
}
