using core.Utilities;
using data_porter.Models.DBModels;
using data_porter.Models.Models.Upload;
using data_porter.Repositories.AzureBlobReferences;
using data_porter.Repositories.AzureBlobs;
using data_porter.Repositories.CreateEntities;
using Newtonsoft.Json.Linq;

namespace data_porter.Processor.AzureBlobs;

/// <summary>
/// Azure Blob Processor
/// </summary>
public class UploadProcessor : IUploadProcessor
{
    private readonly IAzureBlobRepository _azureBlobRepo;
    private readonly IAzureBlobReferenceRepository _azureBlobReferenceRepo;
    private readonly ICreateEntityRepositoty _createEntityRepositoty;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="azureBlobRepo">Azure Blob Repository <see cref="IAzureBlobRepository"/></param>
    /// <param name="azureBlobReferenceRepo">Azure Blob Reference Repository <see cref="IAzureBlobReferenceRepository"/></param>
    /// <param name="createEntityRepositoty">Create Entity Repository <see cref="ICreateEntityRepositoty"/></param>
    public UploadProcessor(IAzureBlobRepository azureBlobRepo,
        IAzureBlobReferenceRepository azureBlobReferenceRepo,
        ICreateEntityRepositoty createEntityRepositoty)
    {
        _azureBlobRepo = azureBlobRepo;
        _azureBlobReferenceRepo = azureBlobReferenceRepo;
        _createEntityRepositoty = createEntityRepositoty;
    }

    /// <inheritdoc/>
    public async Task<UploadResponse> Upload(UploadRequest request)
    {
        var fileId = Guid.NewGuid();

        var azureBlobresponse = await _azureBlobRepo.Upload(fileId.ToString(), request.File);
        if (azureBlobresponse.Errors.Any())
            return new UploadResponse(azureBlobresponse.Errors);

        var azureBlobReferenceResponse = await _azureBlobReferenceRepo.SaveAzureBlobReference(
            new AzureBlobReference
            {
                Id = fileId,
                FileName = request.File.FileName,
                FileSize = request.File.Length,
                FileType = request.File.ContentType
            });
        if (azureBlobReferenceResponse.Errors.Any())
            return new UploadResponse(azureBlobReferenceResponse.Errors);

        var jsonString = await FileUtility.ReadFileContentsAsync(request.File);
        JArray jsonArray = JArray.Parse(jsonString);

        var createTableResponse = await _createEntityRepositoty.CreateEntityAsync(fileId.ToString(), jsonArray);
        if (createTableResponse.Errors.Any())
            return new UploadResponse(createTableResponse.Errors);

        return azureBlobReferenceResponse;
    }
}
