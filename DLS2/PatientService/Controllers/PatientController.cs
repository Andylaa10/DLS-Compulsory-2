using Microsoft.AspNetCore.Mvc;
using PatientService.Core.Entities;
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
    [Route("/{ssn}")]
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
    [Route("AddPatient")]
    public async Task<IActionResult> AddPatient([FromBody] Patient patient)
    {
        try
        {
            await _patientService.CreatePatient(patient);
            return StatusCode(201, "Successfully added patient to db");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("DeletePatient/{{ssn}}")]
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
    
    
    [HttpGet]
    [Route("/rebuild")]
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