using core.Extensions;
using data_porter.Models.AzureBlobs.Upload;
using data_porter.Models.Enums;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace data_porter.Managers.AzureBlobs;

/// <summary>
/// Upload Validator
/// </summary>
public class UploadValidator : AbstractValidator<UploadRequest>
{
    private readonly ILogger<UploadValidator> _logger;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="logger">Implements <see cref="ILogger{UploadValidator}"/></param>
    public UploadValidator(ILogger<UploadValidator> logger)
    {
        _logger = logger;
        RuleFor(request => request.FileName).NotNull().NotEmpty()
            .WithErrorCode(((int)ErrorCode.FileNameCannotBeLeftBlank).ToString())
            .WithMessage(ErrorCode.FileNameCannotBeLeftBlank.GetDescription());

        RuleFor(request => request.File).NotNull().NotEmpty()
            .WithErrorCode(((int)ErrorCode.FileCannotBeNull).ToString())
            .WithMessage(ErrorCode.FileCannotBeNull.GetDescription());
    }

    /// <summary>
    /// ValidateAsync
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public override async Task<ValidationResult> ValidateAsync(ValidationContext<UploadRequest> context, CancellationToken cancellation = default)
    {
        var result = await base.ValidateAsync(context, cancellation);

        var request = context.InstanceToValidate;

        if (request.File.ContentType.Contains("application/json"))
        {
            if (!await ValidateJson(request))
            {
                result.Errors.Add(new ValidationFailure(((int)ErrorCode.InvalidJson).ToString(), ErrorCode.InvalidJson.GetDescription()));
            }
        }

        return result;
    }

    private async Task<bool> ValidateJson(UploadRequest request)
    {
        string jsonString = await ReadFileContentsAsync(request.File);
        try
        {
            using JsonDocument document = JsonDocument.Parse(jsonString);
            return true;
        }
        catch (JsonException ex)
        {
            _logger.LogError($"An error occured while validating the JSON {ex.Message}", ex);
            return false;
        }
    }

    private async static Task<string> ReadFileContentsAsync(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        return await reader.ReadToEndAsync();
    }
}
