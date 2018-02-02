
namespace CosmeticsShop.Service.Models.ShoppingCart
{
    using Data.Models;
    using Common.Mapping;
    using System.Collections.Generic;
    using System;
    using AutoMapper;
    using System.Linq;

    public class ShoppingCartItem : IMapFrom<Product>, IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsOnSalle { get; set; }
        public decimal Price { get; set; }
        public decimal SpecialPrice { get; set; }
        public decimal CurentPrice { get; set; }
        public int Quantity { get; set; }
        public string MainImageUrlPath { get; set; }

        public void ConfigureMapping(Profile mapper)
        => mapper
            .CreateMap<Product, ShoppingCartItem>()
        .ForMember(pr => pr.IsOnSalle, cfg => cfg
        .MapFrom(pr => pr.SpecialPriceStartDate < DateTime.UtcNow && pr.SpecialPriceEndDate > DateTime.UtcNow))
        .ForMember(pr => pr.MainImageUrlPath, cfg => cfg
        .MapFrom(pr => pr.Images.Select(i=>i.ImageUrl).FirstOrDefault()));



    }
}
