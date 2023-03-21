using core.Extensions;
using core.Models.DefaultResponses;
using data_porter.Models.DBModels;
using data_porter.Models.Enums;
using data_porter.Models.Models.Upload;
using data_porter.Repositories.AzureBlobReferences;

namespace data_porter.Validators.AzureBlobReferences;

/// <summary>
/// Azure Blob Reference Validator
/// </summary>
public class AzureBlobReferenceValidator : AzureBlobReferenceDecorator
{
    private readonly IAzureBlobReferenceRepository _target;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="target">Implements <see cref="IAzureBlobReferenceRepository"/></param>
    public AzureBlobReferenceValidator(IAzureBlobReferenceRepository target)
    {
        _target = target;
    }

    /// <inheritdoc />
    public override async Task<UploadResponse> SaveAzureBlobReference(AzureBlobReference azureBlobReference)
    {
        if (azureBlobReference.FileName is null)
            return new UploadResponse(new List<ErrorInfo> { new ErrorInfo((int)ErrorCode.FileNameCannotBeLeftBlank, ErrorCode.FileNameCannotBeLeftBlank.GetDescription()) });

        return await _target.SaveAzureBlobReference(azureBlobReference);
    }
}
