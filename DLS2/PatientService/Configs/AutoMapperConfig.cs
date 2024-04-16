using AutoMapper;
using PatientService.Core.Entities;
using PatientService.Core.Services.DTOs;

namespace PatientService.Configs;

public static class AutoMapperConfig
{
    public static IMapper ConfigureAutomapper()
    {
        var mapper = new MapperConfiguration(config =>
        {
            //DTO to Entity
            config.CreateMap<CreatePatientDto, Patient>();

        }).CreateMapper();

        return mapper;
    }
}