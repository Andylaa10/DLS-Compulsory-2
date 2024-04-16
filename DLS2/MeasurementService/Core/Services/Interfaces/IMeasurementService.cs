using System.Diagnostics.Metrics;
using MeasurementService.Core.DTOs;
using MeasurementService.Core.Entities;

namespace MeasurementService.Core.Services.Interfaces;

public interface IMeasurementService
{
    public Task<IEnumerable<Measurement>> GetAllMeasurementsBySsn(string ssn);
    public Task<Measurement> CreateMeasurement(Measurement measurement);
    public Task DeleteMeasurement(int id);
    public Task UpdateMeasurement(int id, UpdateMeasurementDTO measurement);
    public Task RebuildDatabase();
}