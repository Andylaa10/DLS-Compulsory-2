using AutoMapper;
using Cache;
using MeasurementService.Core.Entities;
using MeasurementService.Core.Entities.Helper;
using MeasurementService.Core.Repositories.Interfaces;
using MeasurementService.Core.Services.DTOs;
using MeasurementService.Core.Services.Interfaces;
using Monitoring;
using OpenTelemetry.Trace;

namespace MeasurementService.Core.Services;

public class MeasurementService : IMeasurementService
{
    
    private readonly IMeasurementRepository _measurementRepository;
    private readonly IMapper _mapper;
    private readonly Tracer _tracer;
    private readonly RedisClient _redisClient;

    public MeasurementService(IMeasurementRepository measurementRepository, IMapper mapper, Tracer tracer, RedisClient redisClient)
    {
        _measurementRepository = measurementRepository;
        _mapper = mapper;
        _tracer = tracer;
        _redisClient = redisClient;
        _redisClient.Connect();
    }

    public async Task<IEnumerable<Measurement>> GetAllMeasurementsBySsn(string ssn)
    {
        using var activity = _tracer.StartActiveSpan("GetAllMeasurementsBySsn");
        
        LoggingService.Log.Information("Called GetAllMeasurementsBySsn function");

        return await _measurementRepository.GetAllMeasurementsBySsn(ssn);
    }

    public async Task<PaginationResult<Measurement>> GetAllMeasurementsBySsnPaginated(string ssn, int pageNumber, int pageSize)
    {
        using var activity = _tracer.StartActiveSpan("GetAllMeasurementsBySsnPaginated");
        
        LoggingService.Log.Information("Called GetAllMeasurementsBySsnPaginated function");
        
        return await _measurementRepository.GetAllMeasurementsBySsnPaginated(ssn, pageNumber, pageSize);
    }
    

    public async Task<Measurement> CreateMeasurement(CreateMeasurementDto measurement)
    {
        using var activity = _tracer.StartActiveSpan("CreateMeasurement");
        
        LoggingService.Log.Information("Called CreateMeasurement function");
        
        return await _measurementRepository.CreateMeasurement(_mapper.Map<Measurement>(measurement));
    }

    public async Task<Measurement> DeleteMeasurement(int id)
    {
        using var activity = _tracer.StartActiveSpan("DeleteMeasurement");
        
        LoggingService.Log.Information("Called DeleteMeasurement function");
        
        return await _measurementRepository.DeleteMeasurement(id);
    }

    public async Task<Measurement> UpdateMeasurement(int id, UpdateMeasurementDto measurement)
    {
        using var activity = _tracer.StartActiveSpan("UpdateMeasurement");
        
        LoggingService.Log.Information("Called UpdateMeasurement function");
        
        return await _measurementRepository.UpdateMeasurement(id, _mapper.Map<Measurement>(measurement));
    }

    public async Task RebuildDatabase()
    {
        using var activity = _tracer.StartActiveSpan("RebuildDatabase");
        
        LoggingService.Log.Information("Called RebuildDatabase function");
        
        await _measurementRepository.RebuildDatabase();
    }
}