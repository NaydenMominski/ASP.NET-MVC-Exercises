
namespace CosmeticsShop.Service.Models.ShoppingCart
{
    using CosmeticsShop.Common.Mapping;
    using System;
    using Data.Models;
    using AutoMapper;

    public class ShoppingCartItem: IMapFrom<Product>, IHaveCustomMapping
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public  void ConfigureMapping(Profile mapper)
           => mapper
           .CreateMap<Product, ShoppingCartItem>()
           .ForMember(pr =>pr.ProductId, cfg => cfg.MapFrom(pr => pr.Id));


    }
}
