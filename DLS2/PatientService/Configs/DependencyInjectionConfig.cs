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
        
        //Caching
        services.AddSingleton(RedisClientFactory.CreateRedisClient());
        
        //Tracing
        var serviceName = "MyTracer";
        
        services.AddOpenTelemetry().Setup();
        services.AddSingleton(TracerProvider.Default.GetTracer(serviceName));
        
        //FeatureHub
        services.AddSingleton(FeatureHubFactory.CreateFeatureHub());
    }
}