using System.Diagnostics.Metrics;
using AutoMapper;
using MeasurementService.Core.DTOs;
using MeasurementService.Core.Entities;
using MeasurementService.Core.Repositories.Interfaces;
using MeasurementService.Core.Services.Interfaces;
using Monitoring;
using OpenTelemetry.Trace;

namespace MeasurementService.Core.Services;

public class MeasurementService : IMeasurementService
{
    
    private readonly IMeasurementRepository _measurementRepository;
    private readonly IMapper _mapper;
    private readonly Tracer _tracer;

    public MeasurementService(IMeasurementRepository measurementRepository, IMapper mapper, Tracer tracer)
    {
        _measurementRepository = measurementRepository;
        _mapper = mapper;
        _tracer = tracer;
    }

    public async Task<IEnumerable<Measurement>> GetAllMeasurementsBySsn(string ssn)
    {
        using var activity = _tracer.StartActiveSpan("GetAllMeasurementsBySsn");
        
        Logging.Log.Information("Called GetAllMeasurementsBySsn function");

        return await _measurementRepository.GetAllMeasurementsBySsn(ssn);
    }

    public async Task<Measurement> CreateMeasurement(Measurement measurement)
    {
        using var activity = _tracer.StartActiveSpan("CreateMeasurement");
        
        Logging.Log.Information("Called CreateMeasurement function");
        
        return await _measurementRepository.CreateMeasurement(measurement);
    }

    public async Task DeleteMeasurement(int id)
    {
        using var activity = _tracer.StartActiveSpan("DeleteMeasurement");
        
        Logging.Log.Information("Called DeleteMeasurement function");
        
        await _measurementRepository.DeleteMeasurement(id);
    }

    public async Task UpdateMeasurement(UpdateMeasurementDTO measurement, int id)
    {
        using var activity = _tracer.StartActiveSpan("UpdateMeasurement");
        
        Logging.Log.Information("Called UpdateMeasurement function");
        
        await _measurementRepository.UpdateMeasurement(_mapper.Map<Measurement>(measurement), id);
    }

    public async Task RebuildDatabase()
    {
        using var activity = _tracer.StartActiveSpan("RebuildDatabase");
        
        Logging.Log.Information("Called RebuildDatabase function");
        
        await _measurementRepository.RebuildDatabase();
    }
}