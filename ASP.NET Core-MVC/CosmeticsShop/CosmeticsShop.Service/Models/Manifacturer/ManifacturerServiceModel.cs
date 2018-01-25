namespace CosmeticsShop.Service.Models.Manifacturer
{
    using System;

   public class ManifacturerServiceModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MainImageUrl { get; set; }

        public bool Published { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

    }
}
