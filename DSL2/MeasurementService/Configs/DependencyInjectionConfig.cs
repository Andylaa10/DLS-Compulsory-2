using MeasurementService.Core.Repositories;
using MeasurementService.Core.Repositories.Interfaces;
using MeasurementService.Core.Services.Interfaces;

namespace MeasurementService.Configs;

public static class DependencyInjectionConfig
{
    public static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        //DI 
        services.AddScoped<IMeasurementRepository, MeasurementRepository>();
        services.AddScoped<IMeasurementService, Core.Services.MeasurementService>();
    }
}