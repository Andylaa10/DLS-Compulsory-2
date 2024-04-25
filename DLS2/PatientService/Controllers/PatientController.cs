using FeatureHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PatientService.Core.Services.DTOs;
using PatientService.Core.Services.Interfaces;

namespace PatientService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly FeatureHubClient _featureHubClient;

    public PatientController(IPatientService patientService, FeatureHubClient featureHubClient)
    {
        _patientService = patientService;
        _featureHubClient = featureHubClient;
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
    [Route("SearchPatients")]
    public async Task<IActionResult> SearchPatients([FromQuery] SearchDto dto)
    {
        try
        {
            var patients = await _patientService.SearchPatients(dto);
            Response.Headers.Add("X-Total-Count", patients.TotalCount.ToString());

            return Ok(patients);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("GetPatientPage")]
    public async Task<IActionResult> GetAllPatientsPage([FromQuery] PaginationRequestDto dto)
    {
        try
        {
            var patients = await _patientService.GetAllPatientsWithPagination(dto);
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
            var country = HttpContext.Request.Headers["country"];
            if (country.IsNullOrEmpty()) return BadRequest("No country provided");
            
            var feature = await _featureHubClient.IsCountryAllowed(country);
            
            if (!feature) return StatusCode(403, $"Method not allowed in {country}");
            
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
            var country = HttpContext.Request.Headers["country"];
            if (country.IsNullOrEmpty()) return BadRequest("No country provided");
            
            var feature = await _featureHubClient.IsCountryAllowed(country);
            
            if (!feature) return StatusCode(403, $"Method not allowed in {country}");

            
            return Ok(await _patientService.DeletePatient(ssn));
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