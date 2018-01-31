namespace CosmeticsShop.Service
{
    using CosmeticsShop.Service.Models.Category;
    using System;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        void Create(string name, string description, bool published);

        TModel ById<TModel>(Guid id) where TModel : class;


        IEnumerable<CategoryBasicModel> All();
    }
}
