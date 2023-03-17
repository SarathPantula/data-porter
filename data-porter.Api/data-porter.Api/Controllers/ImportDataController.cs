using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace data_porter.Api.Controllers;

/// <summary>
/// Import Data Controller, this controller is responsible for uploading and downloading files
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ImportDataController : ControllerBase
{

    private readonly CloudBlobContainer _blobContainer;
    //private readonly string _connectionString = "your postgres connection string";
    /// <summary>
    /// 
    /// </summary>
    public ImportDataController()
    {
        // Initialize Azure Blob Storage
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse("");
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        _blobContainer = blobClient.GetContainerReference("excel");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ImportData([FromForm] IFormFile file)
    {
        // Read the Excel file from the request
        using Stream stream = file.OpenReadStream();

        // Generate a unique ID for the file
        Guid fileId = Guid.NewGuid();

        // Upload the file to Azure Blob Storage
        CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(fileId.ToString());
        blockBlob.Properties.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        await blockBlob.UploadFromStreamAsync(stream);

        // Store the ID in Postgres database
        //using var connection = new NpgsqlConnection(_connectionString);
        //connection.Open();

        //using var cmd = new NpgsqlCommand("INSERT INTO ExcelFiles (Id) VALUES (@id)", connection);
        //cmd.Parameters.AddWithValue("id", fileId);
        //await cmd.ExecuteNonQueryAsync();

        return Ok(new { fileId });
    }

    /// <summary>
    /// Download File
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("files/{id}")]
    public async Task<IActionResult> DownloadFile(string id)
    {
        // Get the file from Azure Blob Storage using its ID
        CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(id);

        if (!await blob.ExistsAsync())
        {
            return NotFound();
        }

        // Set the response headers
        Response.Headers.Add("Content-Disposition", $"attachment; filename={blob.Name}");
        Response.Headers.Add("Content-Type", blob.Properties.ContentType);

        // Download the file and return it as a stream
        Stream stream = await blob.OpenReadAsync();
        return File(stream, blob.Properties.ContentType, blob.Name);
    }

}
