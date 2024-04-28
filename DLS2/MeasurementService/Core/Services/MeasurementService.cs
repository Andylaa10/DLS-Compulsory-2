using AutoMapper;
using Cache;
using MeasurementService.Core.Entities;
using MeasurementService.Core.Entities.Helper;
using MeasurementService.Core.Repositories.Interfaces;
using MeasurementService.Core.Services.DTOs;
using MeasurementService.Core.Services.Interfaces;

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

    public async Task<Measurement> GetMeasurementById(int id)
    {
        var measurementJson = _redisClient.GetValue(id.ToString());
        if (!string.IsNullOrEmpty(measurementJson))
            return await Task.FromResult(_redisClient.DeserializeObject<Measurement>(measurementJson)!);

        return await _measurementRepository.GetMeasurementById(id);
    }


    public async Task<Measurement> CreateMeasurement(CreateMeasurementDto dto)
    {
        var measurement = await _measurementRepository.CreateMeasurement(_mapper.Map<Measurement>(dto));

        var measurementJson =  _redisClient.SerializeObject(measurement);

        _redisClient.StoreValue(measurement.Id.ToString(), measurementJson);
        
        return measurement;
    }

    public async Task<Measurement> DeleteMeasurement(int id)
    {
        return await _measurementRepository.DeleteMeasurement(id);
    }

    public async Task<Measurement> UpdateMeasurement(int id, UpdateMeasurementDto dto)
    {
        var measurement = await _measurementRepository.UpdateMeasurement(id, _mapper.Map<Measurement>(dto));
        
        var measurementJson =  _redisClient.SerializeObject(measurement);

        _redisClient.StoreValue(measurement.Id.ToString(), measurementJson);

        return measurement;
    }

    public async Task DeleteMeasurementsOnPatient(string ssn)
    {
        await _measurementRepository.DeleteMeasurementsOnPatient(ssn);
    }

    public async Task RebuildDatabase()
    {
        await _measurementRepository.RebuildDatabase();
    }
}