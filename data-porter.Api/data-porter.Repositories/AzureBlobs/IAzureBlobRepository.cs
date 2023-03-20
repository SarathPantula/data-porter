using data_porter.Models.AzureBlobs.Upload;
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
    /// <param name="file">File Implements <see cref="IFormFile"/></param>
    Task<UploadResponse> Upload(string fileId, IFormFile file);
}
