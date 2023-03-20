using core.Models.DefaultResponses;

namespace data_porter.Models.Models.Upload.AzureBlobs;

/// <summary>
/// Upload Response
/// </summary>
public class AzureBlobResponse : IDefaultResponse
{
    /// <summary>
    /// ctor
    /// </summary>
    public AzureBlobResponse()
    {
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="fileId">File Id</param>
    public AzureBlobResponse(string fileId)
    {
        FileId = fileId;
    }
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="errors">List of Errors. <see cref="List{ErrorInfo}"/></param>
    public AzureBlobResponse(List<ErrorInfo> errors)
    {
        Errors = errors;
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="fileId">File Id</param>
    /// <param name="errors">List of Errors. <see cref="List{ErrorInfo}"/></param>
    public AzureBlobResponse(string fileId, List<ErrorInfo> errors)
    {
        FileId = fileId;
        Errors = errors;
    }


    /// <summary>
    /// File Id
    /// </summary>
    public string FileId { get; set; } = null!;
    /// <summary>
    /// Errors
    /// </summary>
    public List<ErrorInfo> Errors { get; set; } = null!;
}
