using MeasurementService.Core.Entities;

namespace MeasurementService.Core.Repositories.Interfaces;

public interface IMeasurementRepository
{
    public Task<IEnumerable<Measurement>> GetAllMeasurementsBySsn(string ssn);
    public Task<IEnumerable<Measurement>> GetAllMeasurementsBySsnPaginated(string ssn, int pageNumber, int pageSize);
    public Task<Measurement> CreateMeasurement(Measurement measurement);
    public Task DeleteMeasurement(int id);
    public Task UpdateMeasurement(int id, Measurement measurement);
    public Task RebuildDatabase();
}