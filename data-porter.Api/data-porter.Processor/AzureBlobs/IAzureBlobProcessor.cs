using data_porter.Models.Models.Upload;
using data_porter.Models.Models.Upload.AzureBlobs;

namespace data_porter.Processor.AzureBlobs;
/// <summary>
/// IAzure Blob Processor
/// </summary>
public interface IAzureBlobProcessor
{
    /// <summary>
    /// Upload
    /// </summary>
    /// <param name="request">Upload Request <see cref="UploadRequest"/></param>
    /// <returns>Returns <see cref="UploadResponse"/></returns>
    Task<UploadResponse> Upload(UploadRequest request);
}
