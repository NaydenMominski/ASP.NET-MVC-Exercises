namespace CosmeticsShop.Web.Models.Manifacturer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ManifacturerFormViewModel
    {
        public Guid Id { get; set; }

        [StringLength(255, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string MainImageUrl { get; set; }

        public bool Published { get; set; }
        
    }
}
