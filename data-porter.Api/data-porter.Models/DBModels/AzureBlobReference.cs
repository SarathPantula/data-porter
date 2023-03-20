namespace data_porter.Models.DBModels;

/// <summary>
/// Azure Blob Reference
/// </summary>
public partial class AzureBlobReference
{
    /// <summary>
    /// File Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// File Name
    /// </summary>
    public string FileName { get; set; } = null!;

    /// <summary>
    /// File size
    /// </summary>

    public long FileSize { get; set; }

    /// <summary>
    /// File Type
    /// </summary>

    public string FileType { get; set; } = null!;

    /// <summary>
    /// Created Date
    /// </summary>

    public DateTime CreatedDate { get; set; }
}
