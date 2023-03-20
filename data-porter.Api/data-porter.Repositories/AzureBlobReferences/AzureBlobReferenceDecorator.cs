using data_porter.Models.DBModels;
using data_porter.Models.Models.Upload;

namespace data_porter.Repositories.AzureBlobReferences;

/// <summary>
/// Azure Blob Decorator
/// </summary>
public abstract class AzureBlobReferenceDecorator : IAzureBlobReferenceRepository
{
    ///inheritdoc
    public abstract Task<UploadResponse> SaveAzureBlobReference(AzureBlobReference azureBlobReference);
}
