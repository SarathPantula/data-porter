using data_porter.Managers.AzureBlobs;
using data_porter.Models.AzureBlobs.Upload;
using data_porter.Repositories.AzureBlobs;
using MediatR;

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
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(UploadRequest)));
        services.AddScoped<IRequestHandler<UploadRequest, UploadResponse>, UploadHandler>();
        services.AddScoped<IAzureBlobRepository, AzureBlobRepository>();

        return services;
    }
}
