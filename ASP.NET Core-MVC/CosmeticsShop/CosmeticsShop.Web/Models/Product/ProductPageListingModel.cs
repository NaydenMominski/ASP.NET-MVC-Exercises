

namespace CosmeticsShop.Web.Models.Product
{
    using Service.Models.Product;
    using System.Collections.Generic;

    public class ProductPageListingModel
    {
        public IEnumerable<ProductsListingModel> Products { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
