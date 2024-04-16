using MeasurementService.Core.Entities;
using MeasurementService.Core.Repositories.Interfaces;
using MeasurementService.Helper;
using Microsoft.EntityFrameworkCore;

namespace MeasurementService.Core.Repositories;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly MeasurementDbContext _context;

    public async Task<IEnumerable<Measurement>> GetAllMeasurementsBySsn(string ssn)
    {
        return await _context.Measurements.Where(m => m.SSN == ssn).ToListAsync();
    }

    public async Task CreateMeasurement(Measurement measurement)
    {
        await _context.Measurements.AddAsync(measurement);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMeasurement(int id)
    {
        var measurement = await _context.Measurements.FirstOrDefaultAsync(m => m.Id == id) ??
                          throw new NullReferenceException();
        _context.Measurements.Remove(measurement);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMeasurement(Measurement measurement, int id)
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