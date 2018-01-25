

namespace CosmeticsShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [MinLength(0)]
        [Range(0, double.MaxValue)]
        public decimal? SpecialPrice { get; set; }

        public DateTime? SpecialPriceStartDate { get; set; }

        public DateTime? SpecialPriceEndDate { get; set; }

        public int StockQuantity { get; set; }

        public bool Published { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid ManifacturerId { get; set; }
        public Manifacturer Manifacturer { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();


    }
}
