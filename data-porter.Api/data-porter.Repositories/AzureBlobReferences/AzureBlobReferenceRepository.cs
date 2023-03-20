using data_porter.Models.DBModels;
using data_porter.Models.Models.Upload;

namespace data_porter.Repositories.AzureBlobReferences;

/// <summary>
/// Azure Blob Reference Repository
/// </summary>
public class AzureBlobReferenceRepository : AzureBlobReferenceDecorator
{
    private readonly DataPorterContext _context;
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="context">DB Context <see cref="DataPorterContext"/></param>
    public AzureBlobReferenceRepository(DataPorterContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public override async Task<UploadResponse> SaveAzureBlobReference(AzureBlobReference azureBlobReference)
    {
        _context.AzureBlobReferences.Add(azureBlobReference);
        await _context.SaveChangesAsync();

        return new UploadResponse(azureBlobReference);
    }
}
