using data_porter.Models.Models.Upload.AzureBlobs;

namespace data_porter.Models.Models.Upload.CreateEntities;

/// <summary>
/// Create Entity Response
/// </summary>
public class CreateEntityResponse : AzureBlobResponse
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="tableName">string Table Name</param>
    public CreateEntityResponse(string tableName) : base(tableName)
    {
    }
}
