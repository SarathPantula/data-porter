using core.Extensions;
using core.Models.DefaultResponses;
using data_porter.Models.AzureBlobs.Upload;
using data_porter.Models.Enums;
using data_porter.Repositories.AzureBlobs;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

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

    /// inheritdoc
    public override async Task<UploadResponse> Upload(string fileId, IFormFile file)
    {
        List<ErrorInfo> errors = new();
        if (file is null)
        {
            errors.Add(new ErrorInfo((int)ErrorCode.FileCannotBeNull, ErrorCode.FileCannotBeNull.GetDescription()));

            return new UploadResponse(errors);
        }

        if (file.ContentType.Contains("application/json"))
        {
            if (!await ValidateJson(file))
            {
                errors.Add(new ErrorInfo((int)ErrorCode.InvalidJson, ErrorCode.InvalidJson.GetDescription()));

                return new UploadResponse(errors);
            }
        }

        await _target.Upload(fileId, file);

        return new UploadResponse(errors);
    }

    private async static Task<bool> ValidateJson(IFormFile file)
    {
        string jsonString = await ReadFileContentsAsync(file);
        try
        {
            using JsonDocument document = JsonDocument.Parse(jsonString);
            return true;
        }
        catch (JsonException)
        {
            //_logger.LogError($"An error occured while validating the JSON {ex.Message}", ex);
            return false;
        }
    }

    private async static Task<string> ReadFileContentsAsync(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        return await reader.ReadToEndAsync();
    }
}
