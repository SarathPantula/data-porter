using data_porter.Models.AzureBlobs.Upload;
using data_porter.Repositories.AzureBlobs;

namespace data_porter.Processor.AzureBlobs;

/// <summary>
/// Azure Blob Processor
/// </summary>
public class AzureBlobProcessor : IAzureBlobProcessor
{
    private readonly IAzureBlobRepository _target;
    /// <summary>
    /// ctor
    /// </summary>
    public AzureBlobProcessor(IAzureBlobRepository target)
    {
        _target = target;
    }

    ///inheritdoc
    public async Task<UploadResponse> Upload(UploadRequest request)
    {
        var fileId = Guid.NewGuid().ToString();

        var response = await _target.Upload(fileId, request.File);

        return new UploadResponse(fileId, response.Errors);
    }
}
