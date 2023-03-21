using data_porter.Models.Models.Upload.AzureBlobs;
using Microsoft.AspNetCore.Http;

namespace data_porter.Repositories.AzureBlobs;

/// <summary>
/// Azure Blob Decorator
/// </summary>
public abstract class AzureBlobDecorator : IAzureBlobRepository
{
    /// <inheritdoc/>
    public abstract Task<AzureBlobResponse> Upload(string fileId, IFormFile file);
}
