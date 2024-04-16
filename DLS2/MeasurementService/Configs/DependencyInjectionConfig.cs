using MeasurementService.Core.Repositories;
using MeasurementService.Core.Repositories.Interfaces;
using MeasurementService.Core.Services.Interfaces;
using Monitoring;
using OpenTelemetry.Trace;

namespace MeasurementService.Configs;

public static class DependencyInjectionConfig
{
    public static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        //DI 
        services.AddScoped<IMeasurementRepository, MeasurementRepository>();
        services.AddScoped<IMeasurementService, Core.Services.MeasurementService>();
        
        services.AddSingleton(AutoMapperConfig.ConfigureAutomapper());
        
        services.AddOpenTelemetry().Setup();
        services.AddSingleton(TracerProvider.Default.GetTracer("MyTracer"));
    }
}