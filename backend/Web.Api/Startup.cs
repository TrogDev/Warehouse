using Warehouse.Application;
using Warehouse.Infrastructure;
using Warehouse.Web.Api.Configurations;
using Warehouse.Web.Api.Middlewares;

namespace Warehouse.Web.Api;

internal class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);
        services.AddWebServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.ConfigureDatabase();

        app.UseMiddleware<ValidationMiddleware>();
        app.UseMiddleware<EntityNotFoundMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
