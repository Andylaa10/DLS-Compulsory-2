using AutoMapper;
using Cache;
using Messaging;
using Messaging.SharedMessages;
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
    private readonly MessageClient _messageClient;


    public PatientService(IPatientRepository patientRepository, IMapper mapper, RedisClient redisClient, MessageClient messageClient)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
        _redisClient = redisClient;
        _messageClient = messageClient;
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
        //Try to get from the cache first
        var patientJson = _redisClient.GetValue(ssn);
        if (!string.IsNullOrEmpty(patientJson))
            return await Task.FromResult(_redisClient.DeserializeObject<Patient>(patientJson)!);
        
        return await _patientRepository.GetPatientBySsn(ssn);
    }

    public async Task<Patient> DeletePatient(string ssn)
    {
        _redisClient.RemoveValue(ssn);
        await _messageClient.Send(new DeletePatientMessage("Delete Patient", ssn), "DeletePatient");
        var patient = await _patientRepository.DeletePatient(ssn);
        return patient;
    }

    public async Task<Patient> CreatePatient(CreatePatientDto dto)
    {
        var patient = await _patientRepository.CreatePatient(_mapper.Map<Patient>(dto));
        var patientJson = _redisClient.SerializeObject(patient);
        
        _redisClient.StoreValue(patient.SSN, patientJson);
        return patient;
    }

    public async Task RebuildDatabase()
    {
        await _patientRepository.RebuildDatabase();
    }
}