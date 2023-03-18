using data_porter.Repositories.AzureBlobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace data_porter.Managers.AzureBlobs;

/// <summary>
/// Azure Blob Manager
/// </summary>
public class AzureBlobManager : IAzureBlobManager
{
    private readonly IAzureBlobRepository _azureBlobRepo;
    private readonly ILogger<AzureBlobManager> _logger;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="azureBlobRepo">Implements <see cref="IAzureBlobRepository"/></param>
    /// <param name="logger">Implements <see cref="ILogger{AzureBlobManager}"/></param>
    public AzureBlobManager(IAzureBlobRepository azureBlobRepo, ILogger<AzureBlobManager> logger)
    {
        _azureBlobRepo = azureBlobRepo;
        _logger = logger;
    }

    /// inheritdoc
    public async Task<string> Upload(IFormFile file)
    {
        if (file is null) return null!;

        string fileId = Guid.NewGuid().ToString();
        string contentType = file.ContentType;

        await _azureBlobRepo.Upload(fileId, contentType, file);

        return fileId;
    }
}
