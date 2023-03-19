using data_porter.Models.AzureBlobs.Upload;
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
    private readonly IMediator _mediator;

    /// <summary>
    /// ctor
    /// </summary>
    public ImportDataController(ILogger<ImportDataController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Imports a file, a file can be of csv, xlsx, json 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ImportData([FromForm] UploadRequest request)
    {
        try
        {
            var response = await _mediator.Send(request);

            if (response.Errors is not null && response.Errors.Any())
                return BadRequest(response);

            return Ok(response );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }
}
