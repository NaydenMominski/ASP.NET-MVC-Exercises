namespace CosmeticsShop.Service
{
    using CosmeticsShop.Service.Models.Category;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        void Create(string name, string description, bool published);

        IEnumerable<CategoryBasicModel> All();
    }
}
