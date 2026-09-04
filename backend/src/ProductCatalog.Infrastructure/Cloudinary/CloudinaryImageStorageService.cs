using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using ProductCatalog.Application.Interfaces;
using AppImageUploadResult = ProductCatalog.Application.Interfaces.ImageUploadResult;

namespace ProductCatalog.Infrastructure.Cloudinary
{
    public class CloudinaryImageStorageService
        : IImageStorageService
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        public CloudinaryImageStorageService(
            IOptions<CloudinarySettings> options)
        {
            var settings = options.Value;

            var account = new Account(
                settings.CloudName,
                settings.ApiKey,
                settings.ApiSecret
            );

            _cloudinary = new CloudinaryDotNet.Cloudinary(account);

            _cloudinary.Api.Secure = true;
        }

        public async Task<AppImageUploadResult> UploadAsync(
            Stream stream,
            string fileName,
            string contentType)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(
                    fileName,
                    stream
                ),

                Folder = "product-catalog/products",
                UniqueFilename = true,
                Overwrite = false
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
            {
                throw new InvalidOperationException(
                    result.Error.Message
                );
            }

            return new AppImageUploadResult(
                result.SecureUrl.ToString(),
                result.PublicId
            );
        }
    }
}