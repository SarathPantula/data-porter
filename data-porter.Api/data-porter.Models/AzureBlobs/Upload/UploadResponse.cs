using core.Models.DefaultResponses;

namespace data_porter.Models.AzureBlobs.Upload;

/// <summary>
/// Upload Response
/// </summary>
public class UploadResponse : IDefaultResponse
{
    /// <summary>
    /// File Id
    /// </summary>
    public string FileId { get; set; } = null!;
    /// <summary>
    /// Errors
    /// </summary>
    public List<ErrorInfo> Errors { get; set; } = null!;
}
