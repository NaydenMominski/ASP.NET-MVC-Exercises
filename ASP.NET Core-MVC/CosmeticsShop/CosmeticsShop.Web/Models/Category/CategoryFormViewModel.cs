namespace CosmeticsShop.Web.Models.Category
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CategoryFormViewModel
    {
        public Guid Id { get; set; }

        [StringLength(255, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Published { get; set; }
    }
}
