namespace CosmeticsShop.Service.Implementations
{
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class ImageService : IImageService
    {
        private readonly CosmeticsShopDbContext db;

        public ImageService(CosmeticsShopDbContext db)
        {
            this.db = db;
        }

        public void EditHomePictures(Guid productId, List<string> imagesUrls)
        {
            //remove old pictures
            this.db
                .Images
                .RemoveRange(this.db
                                .Images
                                .Where(p => p.ProductId == productId));

            //save new ones
            foreach (var url in imagesUrls)
            {
                this.db.Images.Add(new Image
                {
                    ImageUrl = url,
                    ProductId = productId                     
                });
            }

            this.db.SaveChanges();
        }

        public string GetBase64(string imageRelativePath)
        {
            string path = this.GetFilePath(imageRelativePath);
            byte[] imagebyte = this.GetFileData(path);
            var base64 = Convert.ToBase64String(imagebyte);

            return base64;
        }

        public byte[] GetFileData(string imagePath)
        {
            FileStream fs = new FileStream(imagePath, FileMode.Open);
            byte[] byteData = new byte[fs.Length];
            fs.Read(byteData, 0, byteData.Length);
            fs.Close();
            return byteData;
        }

        public string GetFilePath(string relativepath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), relativepath);
        }

        public string GetProductPicturesFullPath(Guid productId)
        {
            var pathTokens = this.db
                .Images
                .FirstOrDefault(p => p.ProductId == productId)
                .ImageUrl
                .Split('/')
                .Take(3).ToArray();

            return this.GetFilePath(string.Join("/", pathTokens));
        }

        public string PreparePictureToDisplay(string relativePath)
        {
            var base64 = this.GetBase64(relativePath);
            return string.Format("data:image;base64,{0}", base64);
        }
    }
}