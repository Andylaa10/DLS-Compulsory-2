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
    private readonly RedisClient _redisClient;

    public MeasurementService(IMeasurementRepository measurementRepository, IMapper mapper, RedisClient redisClient)
    {
        _measurementRepository = measurementRepository;
        _mapper = mapper;
        _redisClient = redisClient;
        _redisClient.Connect();
    }

    public async Task<IEnumerable<Measurement>> GetAllMeasurementsBySsn(string ssn)
    {
        return await _measurementRepository.GetAllMeasurementsBySsn(ssn);
    }

    public async Task<PaginationResult<Measurement>> GetAllMeasurementsBySsnPaginated(string ssn, int pageNumber, int pageSize)
    {
        return await _measurementRepository.GetAllMeasurementsBySsnPaginated(ssn, pageNumber, pageSize);
    }
    

    public async Task<Measurement> CreateMeasurement(CreateMeasurementDto measurement)
    {
        return await _measurementRepository.CreateMeasurement(_mapper.Map<Measurement>(measurement));
    }

    public async Task<Measurement> DeleteMeasurement(int id)
    {
        return await _measurementRepository.DeleteMeasurement(id);
    }

    public async Task<Measurement> UpdateMeasurement(int id, UpdateMeasurementDto measurement)
    {
        
        return await _measurementRepository.UpdateMeasurement(id, _mapper.Map<Measurement>(measurement));
    }

    public async Task RebuildDatabase()
    {
        await _measurementRepository.RebuildDatabase();
    }
}