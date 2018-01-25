namespace CosmeticsShop.Service.Models.Product
{
    using Data.Models;
    using Common.Mapping;
    using AutoMapper;
    using System.Linq;
    using System.Collections.Generic;

    public class ProductWithImagesServiceModel : IMapFrom<Product>, IHaveCustomMapping
    {

        public string Images111 { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<Product, ProductWithImagesServiceModel>()
            .ForMember(p => p.Images111, cfg => cfg
            .MapFrom(p => p.Images.Select(i => i.ImageUrl).FirstOrDefault()));
    }
}
