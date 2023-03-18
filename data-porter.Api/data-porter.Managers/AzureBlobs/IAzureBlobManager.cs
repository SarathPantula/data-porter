using Microsoft.AspNetCore.Http;

namespace data_porter.Managers.AzureBlobs;

/// <summary>
/// Azure Blob Manager
/// </summary>
public interface IAzureBlobManager
{
    /// <summary>
    /// File Upload
    /// </summary>
    /// <param name="file">File implements <see cref="IFormFile"/></param>
    /// <returns>file Id</returns>
    Task<string> Upload(IFormFile file);
}
