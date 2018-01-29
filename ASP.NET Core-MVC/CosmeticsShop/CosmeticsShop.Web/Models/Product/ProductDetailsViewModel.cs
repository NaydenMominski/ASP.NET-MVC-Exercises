


namespace CosmeticsShop.Web.Models.Product
{
    using CosmeticsShop.Data.Models;
    using System;
    using System.Collections.Generic;

    public class ProductDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}
