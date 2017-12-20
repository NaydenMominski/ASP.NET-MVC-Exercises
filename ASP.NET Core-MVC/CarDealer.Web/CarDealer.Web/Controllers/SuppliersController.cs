
namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Suppliers;
    using Services;
    using Services.Models;

    public class SuppliersController:Controller
    {
        private const string SuppliersView = "Suppliers";

        private readonly ISupplierService suppliers;

        public SuppliersController(ISupplierService suppliers)
        {
            this.suppliers = suppliers;
        }

        public ActionResult Local()
            => View(SuppliersView, this.GetSuppliers(false));
       
        public ActionResult Importers()
            => View(SuppliersView, this.GetSuppliers(true));
        

        private SuppliersModel GetSuppliers(bool importers)
        {
            var type = importers ? "Importer" : "Local";
            var suppliers = this.suppliers.All(importers);

            return new SuppliersModel
            {
                Type = type,
                Suppliers = suppliers
            };
        }
    }
}
