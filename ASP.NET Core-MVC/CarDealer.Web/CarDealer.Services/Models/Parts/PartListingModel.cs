namespace CarDealer.Services.Models.Parts
{
    using System.Collections.Generic;
    using System.Linq;

    public class PartListingModel: PartModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public string SupplierName { get; set; }
    }
}
