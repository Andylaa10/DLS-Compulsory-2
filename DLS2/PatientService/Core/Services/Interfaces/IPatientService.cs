﻿using PatientService.Core.Entities;
using PatientService.Core.Services.DTOs;

namespace PatientService.Core.Services.Interfaces;

public interface IPatientService
{
    public Task<IEnumerable<Patient>> GetAllPatients();
    public Task<Patient> GetPatientBySsn(string ssn);
    public Task DeletePatient(string ssn);
    public Task<Patient> CreatePatient(CreatePatientDto patient);
    public Task RebuildDatabase();
}