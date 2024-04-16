using AutoMapper;
using MeasurementService.Core.DTOs;
using MeasurementService.Core.Entities;

namespace MeasurementService.Configs;

public static class AutoMapperConfig
{
    public static IMapper ConfigureAutomapper()
    {
        var mapper = new MapperConfiguration(configure =>
        {
            configure.CreateMap<UpdateMeasurementDTO, Measurement>();
        }).CreateMapper();

        return mapper;
    }
}