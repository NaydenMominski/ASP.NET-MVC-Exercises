namespace CosmeticsShop.Service
{
    using Models.Manifacturer;
    using System.Collections.Generic;

    public interface IManifacturerService
    {
        void Create(string name, string description, string mainImageUrl, bool published);

        IEnumerable<ManifacturerBasicModel> All();
    }
}
