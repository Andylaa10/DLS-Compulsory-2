using System.Diagnostics.Metrics;
using MeasurementService.Core.Entities;
using MeasurementService.Core.Helper;
using MeasurementService.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace MeasurementService.Core.Repositories;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly MeasurementDbContext _context;

    public MeasurementRepository(MeasurementDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Measurement>> GetAllMeasurementsBySsn(string ssn)
    {
        return await _context.Measurements.Where(m => m.SSN == ssn).ToListAsync();
    }

    public async Task<PaginationResult<Measurement>> GetAllMeasurementsBySsnPaginated(string ssn, int pageNumber, int pageSize)
    {
        var query = _context.Measurements.Where(m => m.SSN == ssn);
        
        int totalCount = await query.CountAsync();

        List<Measurement> measurements = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var result = new PaginationResult<Measurement>
        {
            Items = measurements,
            TotalCount = totalCount
        };

        return await Task.Run(() => result);
    }

    public async Task<Measurement> CreateMeasurement(Measurement measurement)
    {
        await _context.Measurements.AddAsync(measurement);
        await _context.SaveChangesAsync();
        return measurement;
    }

    public async Task DeleteMeasurement(int id)
    {
        var measurement = await _context.Measurements.FirstOrDefaultAsync(m => m.Id == id) ??
                          throw new NullReferenceException();
        _context.Measurements.Remove(measurement);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMeasurement(int id, Measurement measurement)
    {
        var measurementToUpdate = await _context.Measurements.FirstOrDefaultAsync(m => m.Id == id) ??
                                  throw new NullReferenceException();

        if (measurement.Id == id)
        {
            measurementToUpdate.ViewedByDoctor = measurement.ViewedByDoctor;
            measurementToUpdate.Diastolic = measurement.Diastolic;
            measurementToUpdate.Systolic = measurement.Systolic;

            _context.Update(measurementToUpdate);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RebuildDatabase()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }
}