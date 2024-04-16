using AutoMapper;
using MeasurementService.Core.Entities;
using MeasurementService.Core.Services.DTOs;

namespace MeasurementService.Configs;

public static class AutoMapperConfig
{
    public static IMapper ConfigureAutomapper()
    {
        var mapper = new MapperConfiguration(configure =>
        {
            //DTO to Entity
            configure.CreateMap<CreateMeasurementDto, Measurement>();
            configure.CreateMap<UpdateMeasurementDto, Measurement>();
        }).CreateMapper();

        return mapper;
    }
}