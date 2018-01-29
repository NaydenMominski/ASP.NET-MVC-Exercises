namespace CosmeticsShop.Service.Models.Product
{
    using Data.Models;
    using Common.Mapping;
    using AutoMapper;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    public class ProductWithImagesServiceModel : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Image> Images { get; set; }
        
    }
}
