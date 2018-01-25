namespace CosmeticsShop.Service.Models.Product
{
    using Common.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductsListingModel 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal? SpecialPrice { get; set; }



        //public virtual void ConfigureMapping(Profile mapper)
        //    => mapper
        //    .CreateMap<Product, ProductsListingModel>()
        //    .ForMember(p => p.MaingImagePath, cfg => cfg
        //    .MapFrom(p => p.Images
        //                   .OrderBy(i => i.ImageUrl)
        //                   .Select(i => i.ImageUrl)
        //                   .FirstOrDefault()));

        public string MaingImagePath { get; set; }

        //public void ConfigureMapping(Profile mapper)
        //{
        //    mapper
        //    .CreateMap<Product, ProductsListingModel>()
        //   .ForMember(p => p.MaingImagePath, cfg => cfg
        //  .MapFrom(p => p.Images.Select(c => c.ImageUrl).FirstOrDefault()));
        //}

        //public void ConfigureMapping(Profile mapper)
        //    => mapper
        //    .CreateMap<Product, ProductsListingModel>()
        //    .ForMember(p => p.MaingImagePath, cfg => cfg
        //    .MapFrom(p => p.Images.Select(c => c.ImageUrl).First()));
    }
}

