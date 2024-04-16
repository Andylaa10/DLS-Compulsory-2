using AutoMapper;
using MeasurementService.Core.DTOs;
using MeasurementService.Core.Entities;
using MeasurementService.Core.Repositories.Interfaces;
using MeasurementService.Core.Services.Interfaces;

namespace MeasurementService.Core.Services;

public class MeasurementService : IMeasurementService
{
    
    private readonly IMeasurementRepository _measurementRepository;
    private readonly IMapper _mapper;

    public MeasurementService(IMeasurementRepository measurementRepository, IMapper mapper)
    {
        _measurementRepository = measurementRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Measurement>> GetAllMeasurementsBySsn(string ssn)
    {
        return await _measurementRepository.GetAllMeasurementsBySsn(ssn);
    }

    public async Task CreateMeasurement(Measurement measurement)
    {
        await _measurementRepository.CreateMeasurement(measurement);
    }

    public async Task DeleteMeasurement(int id)
    {
        await _measurementRepository.DeleteMeasurement(id);
    }

    public async Task UpdateMeasurement(UpdateMeasurementDTO measurement, int id)
    {
        await _measurementRepository.UpdateMeasurement(_mapper.Map<Measurement>(measurement), id);
    }

    public async Task RebuildDatabase()
    {
        await _measurementRepository.RebuildDatabase();
    }
}