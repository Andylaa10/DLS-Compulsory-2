using PatientService.Core.Repositories;
using PatientService.Core.Repositories.Interfaces;
using PatientService.Core.Services.Interfaces;
using PatientService.Helper;

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
    }
}