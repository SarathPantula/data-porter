using data_porter.Models.Models.Upload;
using data_porter.Models.Models.Upload.CreateEntities;
using Newtonsoft.Json.Linq;

namespace data_porter.Repositories.CreateEntities;

/// <summary>
/// Create Entity Decorator
/// </summary>
public abstract class CreateEntityDecorator : ICreateEntityRepositoty
{
    /// <inheritdoc/>
    public abstract Task<CreateEntityResponse> CreateEntityAsync(string fileName, JArray jsonArray);
}
