using data_porter.Models.AzureBlobs.Upload;
using data_porter.Repositories.AzureBlobs;
using MediatR;

namespace data_porter.Managers.AzureBlobs;

/// <summary>
/// Upload Handler
/// </summary>
public class UploadHandler : IRequestHandler<UploadRequest, UploadResponse>
{
    private readonly IAzureBlobRepository _azureBlobRepo;
    /// <summary>
    /// ctor
    /// </summary>
    public UploadHandler(IAzureBlobRepository azureBlobRepo)
    {
        _azureBlobRepo = azureBlobRepo;
    }

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request">Upload Request</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Returns <see cref="UploadResponse"/></returns>
    public async Task<UploadResponse> Handle(UploadRequest request, CancellationToken cancellationToken)
    {
        if (request.File is null) return null!;

        string fileId = Guid.NewGuid().ToString();

        await _azureBlobRepo.Upload(fileId, request.File.ContentType, request.File);

        return new UploadResponse
        {
            FileId = fileId
        };
    }
}
