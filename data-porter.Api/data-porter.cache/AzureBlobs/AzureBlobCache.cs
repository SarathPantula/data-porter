using data_porter.Models.AzureBlobs.Upload;
using data_porter.Repositories.AzureBlobs;
using Microsoft.AspNetCore.Http;

namespace data_porter.cache.AzureBlobs;

/// <summary>
/// Azure Blob Cache
/// </summary>
public class AzureBlobCache : AzureBlobDecorator
{
    private readonly IAzureBlobRepository _target;
    /// <summary>
    /// ctor
    /// </summary>
    public AzureBlobCache(IAzureBlobRepository target)
    {
        _target = target;
    }

    /// inheritdoc
    public override Task<UploadResponse> Upload(string fileId, IFormFile file)
    {
        return _target.Upload(fileId, file);
    }
}
