using data_porter.Models.Models.Upload.CreateEntities;
using data_porter.Repositories.CreateEntities;
using Newtonsoft.Json.Linq;

namespace data_porter.cache.CreateEntities;

/// <summary>
/// Create Entity Cache
/// </summary>
public class CreateEntityCache : CreateEntityDecorator
{
    private readonly ICreateEntityRepositoty _target;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="target">Implements <see cref="ICreateEntityRepositoty"/></param>
    public CreateEntityCache(ICreateEntityRepositoty target)
    {
        _target = target;
    }

    /// <inheritdoc/>
    public override Task<CreateEntityResponse> CreateEntityAsync(string fileName, JArray jsonArray)
    {
        return _target.CreateEntityAsync(fileName, jsonArray);
    }
}
