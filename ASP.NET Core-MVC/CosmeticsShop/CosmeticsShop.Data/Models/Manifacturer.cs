namespace CosmeticsShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Threading.Tasks;

    public class Manifacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(255, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string MainImageUrl { get; set; }

        public bool Published { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

    }
}
