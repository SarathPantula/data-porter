using Microsoft.AspNetCore.Http;

namespace data_porter.Repositories.AzureBlobs;

/// <summary>
/// IAzureBlobRepository
/// </summary>
public interface IAzureBlobRepository
{
    /// <summary>
    /// Upload File
    /// </summary>
    /// <param name="fileId">File ID</param>
    /// <param name="contentType">Content type</param>
    /// <param name="file">File</param>
    Task Upload(string fileId, string contentType, IFormFile file);
}
