using data_porter.Models.Models.Upload;
using data_porter.Models.Models.Upload.AzureBlobs;
using data_porter.Processor.AzureBlobs;
using data_porter.Repositories.AzureBlobs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace data_porter.Api.Controllers;

/// <summary>
/// Import Data Controller, this controller is responsible for uploading and downloading files
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ImportDataController : ControllerBase
{
    private readonly ILogger<ImportDataController> _logger;
    private readonly IAzureBlobProcessor _azureBlobProcessor;
    /// <summary>
    /// ctor
    /// </summary>
    public ImportDataController(ILogger<ImportDataController> logger,
        IAzureBlobProcessor azureBlobProcessor)
    {
        _logger = logger;
        _azureBlobProcessor = azureBlobProcessor;
    }

    /// <summary>
    /// Imports a file, a file can be of csv, xlsx, json 
    /// </summary>
    /// <returns>Returns <see cref="UploadResponse"/></returns>
    [HttpPost]
    [Route("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ImportData([FromForm] UploadRequest request)
    {
        try
        {
            var response = await _azureBlobProcessor.Upload(request);

            if (response.Errors is not null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occured while validating the JSON {ex.Message}", ex);
            throw;
        }
    }
}
