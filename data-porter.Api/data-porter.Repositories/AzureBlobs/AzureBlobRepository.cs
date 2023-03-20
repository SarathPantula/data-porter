using core.Models.AppSettings;
using data_porter.Models.AzureBlobs.Upload;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace data_porter.Repositories.AzureBlobs;

/// <summary>
/// Azure Blob Storage Repository
/// </summary>
public class AzureBlobRepository : AzureBlobDecorator
{
    private readonly CloudBlobContainer _blobContainer;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="connectionStringSettings">Connection strings IOptions <see cref="ConnectionStringSettings"/></param>
    /// <param name="azureBlobSettings">Azure Blob Settings IOptions <see cref="AzureBlobSettings"/></param>
    public AzureBlobRepository(IOptions<ConnectionStringSettings> connectionStringSettings,
        IOptions<AzureBlobSettings> azureBlobSettings)
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionStringSettings.Value.AzureBlobStorageConnectionString);
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        _blobContainer = blobClient.GetContainerReference(azureBlobSettings.Value.ContainerName);
    }

    ///inheritdoc
    public override async Task<UploadResponse> Upload(string fileId, IFormFile file)
    {
        CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(fileId);
        blockBlob.Properties.ContentType = file.ContentType;

        using Stream stream = file.OpenReadStream();
        await blockBlob.UploadFromStreamAsync(stream);

        return new UploadResponse();
    }
}
