using data_porter.Models.AzureBlobs.Upload;
using Microsoft.AspNetCore.Http;

namespace data_porter.Repositories.AzureBlobs;

/// <summary>
/// Azure Blob Decorator
/// </summary>
public abstract class AzureBlobDecorator : IAzureBlobRepository
{
    ///inheritdoc
    public abstract Task<UploadResponse> Upload(string fileId, IFormFile file);
}
