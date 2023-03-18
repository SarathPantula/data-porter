using core.Models.AppSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace data_porter.Repositories.AzureBlobs;

/// <summary>
/// Azure Blob Storage Repository
/// </summary>
public class AzureBlobRepository : IAzureBlobRepository
{
    private readonly CloudBlobContainer _blobContainer;
    private readonly ILogger<AzureBlobRepository> _logger;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="connectionStringSettings">Connection strings IOptions <see cref="ConnectionStringSettings"/></param>
    /// <param name="azureBlobSettings">Azure Blob Settings IOptions <see cref="AzureBlobSettings"/></param>
    /// <param name="logger">Serilog logging implements <see cref="ILogger{AzureBlobRepository}"/></param>
    public AzureBlobRepository(IOptions<ConnectionStringSettings> connectionStringSettings,
        IOptions<AzureBlobSettings> azureBlobSettings,
        ILogger<AzureBlobRepository> logger)
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionStringSettings.Value.AzureBlobStorageConnectionString);
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        _blobContainer = blobClient.GetContainerReference(azureBlobSettings.Value.ContainerName);
        _logger = logger;
    }

    /// inheritdoc
    public async Task Upload(string fileId, string contentType, IFormFile file)
    {
        CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(fileId);
        blockBlob.Properties.ContentType = contentType;

        using Stream stream = file.OpenReadStream();
        await blockBlob.UploadFromStreamAsync(stream);
    }
}
