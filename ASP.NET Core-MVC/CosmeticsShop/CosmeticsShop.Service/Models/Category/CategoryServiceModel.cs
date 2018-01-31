namespace CosmeticsShop.Service.Models.Category
{
    using CosmeticsShop.Common.Mapping;
    using System;
    using Data.Models;

    public class CategoryServiceModel : IMapFrom<Category>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Published { get; set; }

    }
}
