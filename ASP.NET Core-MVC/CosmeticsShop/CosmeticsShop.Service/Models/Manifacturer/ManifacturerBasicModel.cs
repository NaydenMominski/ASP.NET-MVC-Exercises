
namespace CosmeticsShop.Service.Models.Manifacturer
{
    using System;
    using Common.Mapping;
    using Data.Models;

    public class ManifacturerBasicModel:IMapFrom<Manifacturer>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
