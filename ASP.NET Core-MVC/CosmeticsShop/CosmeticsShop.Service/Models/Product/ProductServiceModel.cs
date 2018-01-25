
namespace CosmeticsShop.Service.Models.Product
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductServiceModel :IMapFrom<Product>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal? SpecialPrice { get; set; }

        public DateTime? SpecialPriceStartDate { get; set; }

        public DateTime? SpecialPriceEndDate { get; set; }

        public int StockQuantity { get; set; }

        public bool Published { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

        public Category Category { get; set; }

        public Manifacturer Manifacturer { get; set; }

        public IEnumerable<Image> Images { get; set; }
    }
}
