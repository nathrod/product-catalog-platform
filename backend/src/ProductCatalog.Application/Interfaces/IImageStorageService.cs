namespace ProductCatalog.Application.Interfaces
{
    public interface IImageStorageService
    {
        Task<ImageUploadResult> UploadAsync(
            Stream stream,
            string fileName,
            string contentType
        );
    }

    public record ImageUploadResult(
        string Url,
        string PublicId
    );
}