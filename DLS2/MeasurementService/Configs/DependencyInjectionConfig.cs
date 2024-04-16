using MeasurementService.Core.Repositories;
using MeasurementService.Core.Repositories.Interfaces;
using MeasurementService.Core.Services.Interfaces;
using MeasurementService.Helper;

namespace MeasurementService.Configs;

public static class DependencyInjectionConfig
{
    public static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        //Database 
        services.AddDbContext<MeasurementDbContext>();
        
        //DI 
        services.AddScoped<IMeasurementRepository, MeasurementRepository>();
        services.AddScoped<IMeasurementService, Core.Services.MeasurementService>();
        services.AddSingleton(AutoMapperConfig.ConfigureAutomapper());
    }
}