namespace CosmeticsShop.Service.Models.Product
{
    using Data.Models;
    using Common.Mapping;
    using System.Collections.Generic;
    using System;
    using AutoMapper;
    using System.Linq;

    public class ProductWithImagesServiceModel : IMapFrom<Product>, IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? SpecialPrice { get; set; }
        public int Images13 { get; set; }

        public virtual void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<Product, ProductWithImagesServiceModel>()
            .ForMember(c => c.Images13, cfg => cfg.MapFrom(c => c.Images.Count));


    }
}
