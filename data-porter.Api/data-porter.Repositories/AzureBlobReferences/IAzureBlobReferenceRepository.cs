using data_porter.Models.DBModels;
using data_porter.Models.Models.Upload;

namespace data_porter.Repositories.AzureBlobReferences;

/// <summary>
/// IAzure Blob Reference Repository
/// </summary>
public interface IAzureBlobReferenceRepository
{
    /// <summary>
    /// Save Azure Blob Reference
    /// </summary>
    /// <param name="azureBlobReference">Azure Blob Reference <see cref="AzureBlobReference"/></param>
    /// <returns></returns>
    Task<UploadResponse> SaveAzureBlobReference(AzureBlobReference azureBlobReference);
}
