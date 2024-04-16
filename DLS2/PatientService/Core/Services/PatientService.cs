using AutoMapper;
using Monitoring;
using OpenTelemetry.Trace;
using PatientService.Core.Entities;
using PatientService.Core.Repositories.Interfaces;
using PatientService.Core.Services.DTOs;
using PatientService.Core.Services.Interfaces;

namespace PatientService.Core.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly Tracer _tracer;
    private readonly IMapper _mapper;

    public PatientService(IPatientRepository patientRepository, Tracer tracer, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _tracer = tracer;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Patient>> GetAllPatients()
    {
        using var activity = _tracer.StartActiveSpan("GetAllPatients");
        
        Logging.Log.Information("Called GetAllPatients function");
        
        return await _patientRepository.GetAllPatients();
    }

    public async Task<Patient> GetPatientBySsn(string ssn)
    {
        using var activity = _tracer.StartActiveSpan("GetPatientBySsn");
        
        Logging.Log.Information("Called GetPatientBySsn function");
        
        return await _patientRepository.GetPatientBySsn(ssn);
    }

    public async Task DeletePatient(string ssn)
    {
        using var activity = _tracer.StartActiveSpan("DeletePatient");
        
        Logging.Log.Information("Called DeletePatient function");
        
        await _patientRepository.DeletePatient(ssn);
    }

    public async Task<Patient> CreatePatient(CreatePatientDto patient)
    {
        using var activity = _tracer.StartActiveSpan("CreatePatient");
        
        Logging.Log.Information("Called CreatePatient function");
        
        return await _patientRepository.CreatePatient(_mapper.Map<Patient>(patient));
    }

    public async Task RebuildDatabase()
    {
        using var activity = _tracer.StartActiveSpan("RebuildDatabase");
        
        Logging.Log.Information("Called RebuildDatabase function");
        
        await _patientRepository.RebuildDatabase();
    }
}