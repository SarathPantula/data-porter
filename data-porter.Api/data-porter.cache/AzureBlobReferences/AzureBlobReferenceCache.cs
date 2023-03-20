using data_porter.Models.DBModels;
using data_porter.Models.Models.Upload;
using data_porter.Repositories.AzureBlobReferences;

namespace data_porter.cache.AzureBlobReferences;

/// <summary>
/// Azure Blob Reference Cache
/// </summary>
public class AzureBlobReferenceCache : AzureBlobReferenceDecorator
{
    private readonly IAzureBlobReferenceRepository _target;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="target">Implements <see cref="IAzureBlobReferenceRepository"/></param>
    public AzureBlobReferenceCache(IAzureBlobReferenceRepository target)
    {
        _target = target;
    }

    /// <inheritdoc/>
    public override async Task<UploadResponse> SaveAzureBlobReference(AzureBlobReference azureBlobReference)
    {
        return await _target.SaveAzureBlobReference(azureBlobReference);
    }
}
