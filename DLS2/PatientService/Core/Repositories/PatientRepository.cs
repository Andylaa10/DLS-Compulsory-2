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

    public async Task<PaginationResult<Patient>> SearchPatients(string searchTerm, int pageNumber, int pageSize)
    {
        var search = searchTerm.ToLower();

        IQueryable<Patient> query = _context.Patients
            .Where(p => p.SSN.ToLower().Contains(search)
                        || p.Email.ToLower().Contains(search)
                        || p.Name.ToLower().Contains(search));

        var totalCount = await query.CountAsync();

        var patients = await query
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginationResult<Patient>
        {
            Items = patients,
            TotalCount = totalCount
        };
    }

    public async Task<PaginationResult<Patient>> GetAllPatientsWithPagination(int pageNumber, int pageSize)
    {
        IQueryable<Patient> query = _context.Patients;

        var totalCount = await query.CountAsync();

        var patients = await query
            .Skip(pageNumber * pageSize)
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

    public async Task<Patient> DeletePatient(string ssn)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.SSN == ssn) ??
                      throw new NullReferenceException();
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();

        return patient;
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