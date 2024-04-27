using MeasurementService.Core.Entities;
using MeasurementService.Core.Entities.Helper;
using MeasurementService.Core.Services.DTOs;

namespace MeasurementService.Core.Services.Interfaces;

public interface IMeasurementService
{
    public Task<IEnumerable<Measurement>> GetAllMeasurementsBySsn(string ssn);
    public Task<Measurement> CreateMeasurement(CreateMeasurementDto measurement);
    public Task<PaginationResult<Measurement>> GetAllMeasurementsBySsnPaginated(string ssn, int pageNumber, int pageSize);
    public Task<Measurement> GetMeasurementById(int id);
    public Task DeleteMeasurement(int id);
    public Task UpdateMeasurement(int id, UpdateMeasurementDto measurement);
    public Task RebuildDatabase();
}