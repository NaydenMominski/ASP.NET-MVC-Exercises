namespace CosmeticsShop.Web.Models.Product
{
    using Service.Models.Product;
    using System;

    public class ProductDetailsViewModel
    {
        public ProductServiceModel Product { get; set; }
        public decimal PercentOfSaving =>this.Product.SpecialPrice.HasValue? (100 - Math.Ceiling((this.Product.SpecialPrice.GetValueOrDefault(1M) / this.Product.Price) * 100)):0;

    }
}
