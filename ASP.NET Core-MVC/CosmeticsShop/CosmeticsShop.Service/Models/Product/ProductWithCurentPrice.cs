namespace CosmeticsShop.Service.Models.Product
{
    using Data.Models;
    using Common.Mapping;
    using System.Collections.Generic;
    using System;
    using AutoMapper;
    using System.Linq;

    public class ProductWithCurentPrice:IMapFrom<Product>, IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsOnSalle { get; set; }
        public decimal CurentPrice { get; set; }

        public void ConfigureMapping(Profile mapper)
        => mapper
            .CreateMap<Product, ProductWithCurentPrice>()
            .ForMember(pr => pr.IsOnSalle, cfg => cfg
            .MapFrom(pr => (pr.SpecialPriceStartDate <= DateTime.UtcNow && pr.SpecialPriceEndDate >= DateTime.UtcNow)))
            .ForMember(pr => pr.CurentPrice, cfg => cfg
            .MapFrom(pr => (this.IsOnSalle) ? pr.Price : pr.SpecialPrice.GetValueOrDefault(0)));
    }
}
