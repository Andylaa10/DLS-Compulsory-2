using FeatureHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Monitoring;
using OpenTelemetry.Trace;
using PatientService.Core.Services.DTOs;
using PatientService.Core.Services.Interfaces;

namespace PatientService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly FeatureHubClient _featureHubClient;
    private readonly Tracer _tracer;


    public PatientController(IPatientService patientService, FeatureHubClient featureHubClient, Tracer tracer)
    {
        _patientService = patientService;
        _featureHubClient = featureHubClient;
        _tracer = tracer;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPatients()
    {
        using var activity = _tracer.StartActiveSpan("GetAllPatients");

        LoggingService.Log.Information("Called GetAllPatients function");

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
        using var activity = _tracer.StartActiveSpan("SearchPatient");

        LoggingService.Log.Information("Called SearchPatient function");

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
        using var activity = _tracer.StartActiveSpan("GetAllPatientsWithPagination");

        LoggingService.Log.Information("Called GetAllPatientsWithPagination function");

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
        using var activity = _tracer.StartActiveSpan("GetPatientBySsn");

        LoggingService.Log.Information("Called GetPatientBySsn function");

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
        using var activity = _tracer.StartActiveSpan("CreatePatient");

        LoggingService.Log.Information("Called CreatePatient function");

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
        using var activity = _tracer.StartActiveSpan("DeletePatient");

        LoggingService.Log.Information("Called DeletePatient function");
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
        using var activity = _tracer.StartActiveSpan("RebuildDatabase");

        LoggingService.Log.Information("Called RebuildDatabase function");

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