﻿using data_porter.cache.AzureBlobReferences;
using data_porter.cache.AzureBlobs;
using data_porter.Processor.AzureBlobs;
using data_porter.Repositories.AzureBlobReferences;
using data_porter.Repositories.AzureBlobs;
using data_porter.Validators.AzureBlobReferences;
using data_porter.Validators.AzureBlobs;
using Microsoft.Extensions.DependencyInjection;

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
}
