using Warehouse.Web.Api.Middlewares;

namespace Warehouse.Web.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddScoped<ValidationMiddleware>();
        services.AddScoped<EntityNotFoundMiddleware>();
        return services;
    }
}
