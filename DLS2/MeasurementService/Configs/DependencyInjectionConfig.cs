using Cache;
using MeasurementService.Core.Helper;
using MeasurementService.Core.Helper.MessageHandlers;
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
        //Database 
        services.AddDbContext<MeasurementDbContext>();

        //DI 
        services.AddScoped<IMeasurementRepository, MeasurementRepository>();
        services.AddScoped<IMeasurementService, Core.Services.MeasurementService>();
        
        //Automapper
        services.AddSingleton(AutoMapperConfig.ConfigureAutomapper());
        
        //Caching
        services.AddSingleton(RedisClientFactory.CreateRedisClient());
        
        //Monitoring
        var serviceName = "PatientService";
        var serviceVersion = "1.0.0";
        services.AddOpenTelemetry().Setup(serviceName, serviceVersion);
        services.AddSingleton(TracerProvider.Default.GetTracer(serviceName));
        
        //Messages handlers
        services.AddHostedService<DeletePatientHandler>();
    }
}