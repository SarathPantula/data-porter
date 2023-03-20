using data_porter.cache.AzureBlobs;
using data_porter.Processor.AzureBlobs;
using data_porter.Repositories.AzureBlobs;
using data_porter.Validators.AzureBlobs;

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
        services.AddScoped<AzureBlobRepository>();
        services.AddScoped<AzureBlobCache>();
        services.AddScoped<AzureBlobValidator>();

        services.AddScoped<IAzureBlobRepository>(provider =>
        {
            IAzureBlobRepository azureBlobRepository = provider.GetRequiredService<AzureBlobRepository>();
            IAzureBlobRepository azureBlobCache = new AzureBlobCache(azureBlobRepository);
            IAzureBlobRepository azureBlobValidator = new AzureBlobValidator(azureBlobCache);

            return azureBlobValidator;
        });

        services.AddScoped<IAzureBlobProcessor, AzureBlobProcessor>();

        return services;
    }
}
