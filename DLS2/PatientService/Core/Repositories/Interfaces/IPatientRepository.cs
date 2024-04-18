using PatientService.Core.Entities;

namespace PatientService.Core.Repositories.Interfaces;

public interface IPatientRepository
{
    public Task<IEnumerable<Patient>> GetAllPatients();

    public Task<PaginationResult<Patient>> SearchPatients(string searchTerm, int pageNumber, int pageSize);
    public Task<PaginationResult<Patient>> GetAllPatientsWithPagination(int pageNumber, int pageSize);
    public Task<Patient> GetPatientBySsn(string ssn);
    
    public Task DeletePatient(string ssn);

    public Task<Patient> CreatePatient(Patient patient);
    
    public Task RebuildDatabase();
}