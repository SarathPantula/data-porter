using data_porter.cache.AzureBlobReferences;
using data_porter.cache.AzureBlobs;
using data_porter.cache.CreateEntities;
using data_porter.Processor.AzureBlobs;
using data_porter.Repositories.AzureBlobReferences;
using data_porter.Repositories.AzureBlobs;
using data_porter.Repositories.CreateEntities;
using data_porter.Validators.AzureBlobReferences;
using data_porter.Validators.AzureBlobs;
using data_porter.Validators.CreateEntities;

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
        RegisterAzureBlobReferenceServices(services);
        RegisterCreateEntityServices(services);

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

        services.AddScoped<IUploadProcessor, UploadProcessor>();

        return services;
    }

    private static IServiceCollection RegisterAzureBlobReferenceServices(IServiceCollection services)
    {
        services.AddScoped<AzureBlobReferenceRepository>();
        services.AddScoped<AzureBlobReferenceCache>();
        services.AddScoped<AzureBlobReferenceValidator>();

        services.AddScoped<IAzureBlobReferenceRepository>(provider =>
        {
            IAzureBlobReferenceRepository azureBlobReferenceRepository = provider.GetRequiredService<AzureBlobReferenceRepository>();
            IAzureBlobReferenceRepository azureBlobReferenceCache = new AzureBlobReferenceCache(azureBlobReferenceRepository);
            IAzureBlobReferenceRepository azureBlobReferenceValidator = new AzureBlobReferenceValidator(azureBlobReferenceCache);

            return azureBlobReferenceValidator;
        });

        return services;
    }

    private static IServiceCollection RegisterCreateEntityServices(IServiceCollection services)
    {
        services.AddScoped<CreateEntityRepository>();
        services.AddScoped<CreateEntityCache>();
        services.AddScoped<CreateEntityValidator>();

        services.AddScoped<ICreateEntityRepositoty>(provider =>
        {
            ICreateEntityRepositoty createEntityRepository = provider.GetRequiredService<CreateEntityRepository>();
            ICreateEntityRepositoty createEntityCache = new CreateEntityCache(createEntityRepository);
            ICreateEntityRepositoty createEntityValidator = new CreateEntityValidator(createEntityCache);

            return createEntityValidator;
        });

        return services;
    }
}
