using data_porter.Models.Models.Upload;
using data_porter.Models.Models.Upload.CreateEntities;
using Newtonsoft.Json.Linq;

namespace data_porter.Repositories.CreateEntities;

/// <summary>
/// ICreate Entity Repository
/// </summary>
public interface ICreateEntityRepositoty
{
    /// <summary>
    /// Create Entity Async
    /// </summary>
    /// <param name="fileName">File name</param>
    /// <param name="jsonArray">Json Array. Implements <see cref="JArray"/></param>
    /// <returns>Returns <see cref="CreateEntityResponse"/></returns>
    Task<CreateEntityResponse> CreateEntityAsync(string fileName, JArray jsonArray);
}
