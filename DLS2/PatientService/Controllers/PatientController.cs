using Microsoft.AspNetCore.Mvc;
using PatientService.Core.Entities;
using PatientService.Core.Services.DTOs;
using PatientService.Core.Services.Interfaces;

namespace PatientService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPatients()
    {
        try
        {
            return Ok(await _patientService.GetAllPatients());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("GetPatientPage")]
    public async Task<IActionResult> GetAllTrucksPage([FromQuery] PaginationRequestDto dto)
    {
        try
        {
            PaginationResult<Patient> patients =
                await _patientService.GetAllPatientsWithPagination(dto.PageNumber, dto.PageSize);
            Response.Headers.Add("X-Total-Count", patients.TotalCount.ToString());

            return Ok(patients);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }


    [HttpGet]
    [Route("{ssn}")]
    public async Task<IActionResult> GetPatientBySsn([FromRoute] string ssn)
    {
        try
        {
            return Ok(await _patientService.GetPatientBySsn(ssn));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("CreatePatient")]
    public async Task<IActionResult> AddPatient([FromBody] CreatePatientDto patient)
    {
        try
        {
            return StatusCode(201, await _patientService.CreatePatient(patient));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("DeletePatient/{ssn}")]
    public async Task<IActionResult> DeletePatient([FromRoute] string ssn)
    {
        try
        {
            await _patientService.DeletePatient(ssn);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [HttpPost]
    [Route("RebuildDb")]
    public async Task<IActionResult> RebuildDatabase()
    {
        try
        {
            await _patientService.RebuildDatabase();
            return StatusCode(200, "Database recreated");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}