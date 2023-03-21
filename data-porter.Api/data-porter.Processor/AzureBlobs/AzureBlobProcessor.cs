using data_porter.Models.DBModels;
using data_porter.Models.Models.Upload;
using data_porter.Models.Models.Upload.AzureBlobs;
using data_porter.Repositories.AzureBlobReferences;
using data_porter.Repositories.AzureBlobs;

namespace data_porter.Processor.AzureBlobs;

/// <summary>
/// Azure Blob Processor
/// </summary>
public class AzureBlobProcessor : IAzureBlobProcessor
{
    private readonly IAzureBlobRepository _azureBlobRepo;
    private readonly IAzureBlobReferenceRepository _azureBlobReferenceRepo;

    /// <summary>
    /// ctor
    /// </summary>
    public AzureBlobProcessor(IAzureBlobRepository azureBlobRepo,
        IAzureBlobReferenceRepository azureBlobReferenceRepo)
    {
        _azureBlobRepo = azureBlobRepo;
        _azureBlobReferenceRepo = azureBlobReferenceRepo;
    }

    /// <inheritdoc/>
    public async Task<UploadResponse> Upload(UploadRequest request)
    {
        var fileId = Guid.NewGuid();

        var azureBlobresponse = await _azureBlobRepo.Upload(fileId.ToString(), request.File);
        if(azureBlobresponse.Errors.Any())
            return new UploadResponse(azureBlobresponse.Errors);

        return await _azureBlobReferenceRepo.SaveAzureBlobReference(
            new AzureBlobReference
        {
            Id = fileId,
            FileName = request.File.FileName,
            FileSize = request.File.Length,
            FileType = request.File.ContentType
        });
    }
}
