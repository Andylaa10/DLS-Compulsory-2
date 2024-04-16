using PatientService.Core.Entities;
using PatientService.Core.Repositories.Interfaces;
using PatientService.Core.Services.Interfaces;

namespace PatientService.Core.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<Patient>> GetAllPatients()
    {
        return await _patientRepository.GetAllPatients();
    }

    public async Task<Patient> GetPatientBySsn(string ssn)
    {
        return await _patientRepository.GetPatientBySsn(ssn);
    }

    public async Task DeletePatient(string ssn)
    {
        await _patientRepository.DeletePatient(ssn);
    }

    public async Task CreatePatient(Patient patient)
    {
        await _patientRepository.CreatePatient(patient);
    }

    public async Task RebuildDatabase()
    {
        await _patientRepository.RebuildDatabase();
    }
}