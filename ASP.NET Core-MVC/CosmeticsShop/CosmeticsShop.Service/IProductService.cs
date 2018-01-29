
namespace CosmeticsShop.Service
{
    using Models.Product;
    using System;
    using System.Collections.Generic;

    public interface IProductService
    {
        //IEnumerable<Manifacturer> WithManifacturer();

        //IEnumerable<Category> WithCategory();

        void Create(
            string name,
            string description,
            decimal price,
            decimal? specialPrice,
            DateTime? specialPriceStartDate,
            DateTime? specialPriceEndDate,
            int stockQuantity,
            bool published,
            Guid CategoryId,
            Guid ManifacturerId,
            IEnumerable<string> imagesPaths
            );


        void Edit(
            Guid id,
            string Name,
            string description,
            decimal price,
            decimal? specialPrice,
            DateTime? specialPriceStartDate,
            DateTime? specialPriceEndDate,
            int stockQuantity,
            bool published,
            Guid CategoryId,
            Guid ManifacturerId,
            IEnumerable<string> images
            );

        ProductServiceModel ById(Guid id);

        void Delete(Guid id);

        IEnumerable<ProductsListingModel> AllListings(int page = 1, int pageSize = 10);
        
        int Total();

        bool Exists(Guid id);

        ProductWithImagesServiceModel Details(Guid id);

    }
}

