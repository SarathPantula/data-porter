using data_porter.Managers.AzureBlobs;
using data_porter.Models.AzureBlobs.Upload;
using Microsoft.AspNetCore.Mvc;

namespace data_porter.Api.Controllers;

/// <summary>
/// Import Data Controller, this controller is responsible for uploading and downloading files
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ImportDataController : ControllerBase
{
    private readonly IAzureBlobManager _azureBlobManager;
    private readonly ILogger<ImportDataController> _logger;

    /// <summary>
    /// ctor
    /// </summary>
    public ImportDataController(IAzureBlobManager azureBlobManager,
        ILogger<ImportDataController> logger)
    {
        _azureBlobManager = azureBlobManager;
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ImportData([FromForm] Request request)
    {
        try
        {
            return Ok(await _azureBlobManager.Upload(request.File));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }

    ///// <summary>
    ///// Download File
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //[HttpGet("files/{id}")]
    //public async Task<IActionResult> DownloadFile(string id)
    //{
    //    // Get the file from Azure Blob Storage using its ID
    //    CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(id);

    //    if (!await blob.ExistsAsync())
    //    {
    //        return NotFound();
    //    }

    //    // Set the response headers
    //    Response.Headers.Add("Content-Disposition", $"attachment; filename={blob.Name}");
    //    Response.Headers.Add("Content-Type", blob.Properties.ContentType);

    //    // Download the file and return it as a stream
    //    Stream stream = await blob.OpenReadAsync();
    //    return File(stream, blob.Properties.ContentType, blob.Name);
    //}
}
