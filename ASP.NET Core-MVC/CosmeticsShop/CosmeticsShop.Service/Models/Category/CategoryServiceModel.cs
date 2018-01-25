namespace CosmeticsShop.Service.Models.Category
{
    using System;

    public class CategoryServiceModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Published { get; set; }

    }
}
