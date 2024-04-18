using Microsoft.EntityFrameworkCore;
using PatientService.Core.Entities;
using PatientService.Core.Helper;
using PatientService.Core.Repositories.Interfaces;

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
        return await _context.Patients.ToListAsync();
    }
    
    public async Task<PaginationResult<Patient>> GetAllPatientsWithPagination(int pageNumber, int pageSize)
    {
        IQueryable<Patient> query = _context.Patients;

        int totalCount = await query.CountAsync();

        List<Patient> patients = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginationResult<Patient>
        {
            Items = patients,
            TotalCount = totalCount
        };
    }
    
    public async Task<Patient> GetPatientBySsn(string ssn)
    {
        return await _context.Patients.FirstOrDefaultAsync(p => p.SSN == ssn) ?? throw new NullReferenceException();
    }

    public async Task DeletePatient(string ssn)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.SSN == ssn) ??
                      throw new NullReferenceException();
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
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }
}