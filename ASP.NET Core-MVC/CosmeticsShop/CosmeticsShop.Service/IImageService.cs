namespace CosmeticsShop.Service
{
    using System;
    using System.Collections.Generic;

    public interface IImageService
    {
        string GetFilePath(string relativepath);

        string GetBase64(string imageRelativePath);

        byte[] GetFileData(string imagePath);

        string GetProductPicturesFullPath(Guid productId);

        void EditHomePictures(Guid productId, List<string> imagesUrls);

        string PreparePictureToDisplay(string relativePath);
    }
}