using data_porter.Managers.AzureBlobs;
using data_porter.Repositories.AzureBlobs;

namespace data_porter.Api.Extensions;

/// <summary>
/// API Service Extension
/// </summary>
public static class APIServiceExtension
{
    /// <summary>
    /// Register API Services
    /// </summary>
    /// <param name="services">Implements <see cref="IServiceCollection"/></param>
    /// <returns></returns>
    public static IServiceCollection RegisterAPIServices(this IServiceCollection services)
    {
        RegisterAzureBlobServices(services);

        return services;
    }

    private static IServiceCollection RegisterAzureBlobServices(IServiceCollection services)
    {
        services.AddScoped<IAzureBlobManager, AzureBlobManager>();
        services.AddScoped<IAzureBlobRepository, AzureBlobRepository>();

        return services;
    }
}
