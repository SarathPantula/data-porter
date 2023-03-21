using core.Extensions;
using core.Models.DefaultResponses;
using core.Utilities;
using data_porter.Models.Enums;
using data_porter.Models.Models.Upload.AzureBlobs;
using data_porter.Repositories.AzureBlobs;
using Microsoft.AspNetCore.Http;

namespace data_porter.Validators.AzureBlobs;

/// <summary>
/// Azure Blob Validator
/// </summary>
public class AzureBlobValidator : AzureBlobDecorator
{
    private readonly IAzureBlobRepository _target;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="target">Implements <see cref="IAzureBlobRepository"/></param>
    public AzureBlobValidator(IAzureBlobRepository target)
    {
        _target = target;
    }

    /// <inheritdoc />
    public override async Task<AzureBlobResponse> Upload(string fileId, IFormFile file)
    {
        List<ErrorInfo> errors = new();
        if (file is null)
        {
            errors.Add(new ErrorInfo((int)ErrorCode.FileCannotBeNull, ErrorCode.FileCannotBeNull.GetDescription()));

            return new AzureBlobResponse(errors);
        }

        if (file.ContentType.Contains("application/json"))
        {
            if (!await JsonUtility.ValidateJson(file))
            {
                errors.Add(new ErrorInfo((int)ErrorCode.InvalidJson, ErrorCode.InvalidJson.GetDescription()));

                return new AzureBlobResponse(errors);
            }
        }

        await _target.Upload(fileId, file);

        return new AzureBlobResponse(errors);
    }
}
