using Cache;
using FeatureHub;
using Monitoring;
using OpenTelemetry.Trace;
using PatientService.Core.Helper;
using PatientService.Core.Repositories;
using PatientService.Core.Repositories.Interfaces;
using PatientService.Core.Services.Interfaces;

namespace PatientService.Configs;

public static class DependencyInjectionConfig
{
    public static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        //Database 
        services.AddDbContext<PatientDbContext>();
        
        //DI
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IPatientService, Core.Services.PatientService>();

        //Automapper
        services.AddSingleton(AutoMapperConfig.ConfigureAutomapper());
        
        //Tracing
        var serviceName = "MyTracer";
        var serviceVersion = "1.0.0";
        
        services.AddOpenTelemetry().Setup();
        services.AddSingleton(TracerProvider.Default.GetTracer(serviceName));
        
        //Caching
        services.AddSingleton(RedisClientFactory.CreateRedisClient());
        
        //FeatureHub
        services.AddSingleton(FeatureHubFactory.CreateFeatureHub());
    }
}