﻿using Cache;
using EasyNetQ;
using FeatureHub;
using Messaging;
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
        //Messaging
        services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));
        
        //Database 
        services.AddDbContext<PatientDbContext>();
        
        //DI
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IPatientService, Core.Services.PatientService>();
        
        //Automapper
        services.AddSingleton(AutoMapperConfig.ConfigureAutomapper());
        
        //Caching
        services.AddSingleton(RedisClientFactory.CreateRedisClient());
        
        //FeatureHub
        services.AddSingleton(FeatureHubFactory.CreateFeatureHub());
        
        //Monitoring
        var serviceName = "PatientService";
        var serviceVersion = "1.0.0"; 
        services.AddOpenTelemetry().Setup(serviceName, serviceVersion);
        services.AddSingleton(TracerProvider.Default.GetTracer(serviceName));
    }
}