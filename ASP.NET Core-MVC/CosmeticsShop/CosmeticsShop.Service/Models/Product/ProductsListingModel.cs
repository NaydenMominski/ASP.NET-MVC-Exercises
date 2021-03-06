﻿namespace CosmeticsShop.Service.Models.Product
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;
    using System.Linq;

    public class ProductsListingModel :IMapFrom<Product>, IHaveCustomMapping
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal? SpecialPrice { get; set; }

        public DateTime? SpecialPriceStartDate { get; set; }

        public DateTime? SpecialPriceEndDate { get; set; }

        public bool IsOnSalle => (this.SpecialPriceStartDate <= DateTime.UtcNow && this.SpecialPriceEndDate >= DateTime.UtcNow);

        public string MaingImagePath { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
            .CreateMap<Product, ProductsListingModel>()
           .ForMember(p => p.MaingImagePath, cfg => cfg
          .MapFrom(p => p.Images.Select(c => c.ImageUrl).FirstOrDefault()));
        }
    }
}

