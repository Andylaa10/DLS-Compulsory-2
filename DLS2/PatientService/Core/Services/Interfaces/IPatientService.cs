using PatientService.Core.Entities;

namespace PatientService.Core.Services.Interfaces;

public interface IPatientService
{
    public Task<IEnumerable<Patient>> GetAllPatients();
    public Task<Patient> GetPatientBySsn(string ssn);
    public Task DeletePatient(string ssn);
    public Task CreatePatient(Patient patient);
    public Task RebuildDatabase();
}