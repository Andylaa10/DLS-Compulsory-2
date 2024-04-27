using AutoMapper;
using Cache;
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
    private readonly IMapper _mapper;
    private readonly RedisClient _redisClient;

    public PatientService(IPatientRepository patientRepository, IMapper mapper, RedisClient redisClient)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
        _redisClient = redisClient;
        _redisClient.Connect();
    }

    public async Task<IEnumerable<Patient>> GetAllPatients()
    {
        return await _patientRepository.GetAllPatients();
    }

    public async Task<PaginationResult<Patient>> SearchPatients(SearchDto dto)
    {
        return await _patientRepository.SearchPatients(dto.SearchTerm, dto.PageNumber, dto.PageSize);
    }

    public async Task<PaginationResult<Patient>> GetAllPatientsWithPagination(PaginationRequestDto dto)
    {
        return await _patientRepository.GetAllPatientsWithPagination(dto.PageNumber, dto.PageSize);
    }


    public async Task<Patient> GetPatientBySsn(string ssn)
    {
        return await _patientRepository.GetPatientBySsn(ssn);
    }

    public async Task<Patient> DeletePatient(string ssn)
    {
        return await _patientRepository.DeletePatient(ssn);
    }

    public async Task<Patient> CreatePatient(CreatePatientDto patient)
    {
        return await _patientRepository.CreatePatient(_mapper.Map<Patient>(patient));
    }

    public async Task RebuildDatabase()
    {
        await _patientRepository.RebuildDatabase();
    }
}