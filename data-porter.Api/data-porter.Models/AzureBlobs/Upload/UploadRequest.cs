using MediatR;
using Microsoft.AspNetCore.Http;

namespace data_porter.Models.AzureBlobs.Upload;

/// <summary>
/// Request
/// </summary>
public class UploadRequest : IRequest<UploadResponse>
{
    /// <summary>
    /// File name
    /// </summary>
    public string FileName { get; set; } = null!;
    /// <summary>
    /// File
    /// </summary>
    public IFormFile File { get; set; } = null!;

}
