using Microsoft.EntityFrameworkCore;
using PatientService.Core.Entities;
using PatientService.Core.Repositories.Interfaces;
using PatientService.Helper;

namespace PatientService.Core.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly PatientDbContext _context;

    public PatientRepository(PatientDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAllPatients()
    {
        //TODO add tracing
        //using var activity = _tracer.StartActiveSpan("GetAllPatients");
        
        //TODO Add logging
        //Logging.Log.Information("Called GetAllPatients function");

        return await _context.Patients.ToListAsync();    }

    public async Task<Patient> GetPatientBySsn(string ssn)
    {
        // TODO Add Tracing
        //using var activity = _tracer.StartActiveSpan("GetPatientBySsn");
        
        // TODO Add Logging
        //Logging.Log.Information("Called GetPatientBySsn function");
        
        
        return await _context.Patients.FirstOrDefaultAsync(p => p.SSN == ssn) ?? throw new NullReferenceException();
    }

    public async Task DeletePatient(string ssn)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.SSN == ssn) ?? throw new NullReferenceException();
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<Patient> CreatePatient(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task RebuildDatabase()
    {
        // TODO add tracing
        // using var activity = _tracer.StartActiveSpan("Rebuild DB");
        
        // TODO add logging
        // Logging.Log.Information("Called RebuildDatabase function");

        await _context.Database.EnsureDeletedAsync(); 
        await _context.Database.EnsureCreatedAsync();
    }
}