
namespace CosmeticsShop.Web.Models.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ProductFormViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? SpecialPrice { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SpecialPriceStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SpecialPriceEndDate { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public bool Published { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }

        public IEnumerable<SelectListItem> AllCategory { get; set; }

        public Guid SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> AllManifacturer { get; set; }

        public Guid SelectedManifacturerId { get; set; }

        public List<IFormFile> Images { get; set; }

    }
}
