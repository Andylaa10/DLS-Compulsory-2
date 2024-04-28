using MeasurementService.Core.Services.DTOs;
using MeasurementService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Monitoring;
using OpenTelemetry.Trace;

namespace MeasurementService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeasurementController : ControllerBase
{
    private readonly IMeasurementService _measurementService;
    private readonly Tracer _tracer;


    public MeasurementController(IMeasurementService measurementService, Tracer tracer)
    {
        _measurementService = measurementService;
        _tracer = tracer;
    }


    [HttpGet]
    [Route("{ssn}")]
    public async Task<IActionResult> GetAllMeasurementsBySsn([FromRoute] string ssn)
    {
        using var activity = _tracer.StartActiveSpan("GetAllMeasurementsBySsn");

        LoggingService.Log.Information("Called GetAllMeasurementsBySsn function");

        try
        {
            return Ok(await _measurementService.GetAllMeasurementsBySsn(ssn));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("GetMeasurementPage")]
    public async Task<IActionResult> GetAllMeasurementsBySsnPaginated([FromRoute] string ssn,
        [FromQuery] PaginationRequestDto dto)
    {
        using var activity = _tracer.StartActiveSpan("GetAllMeasurementsBySsnPaginated");

        LoggingService.Log.Information("Called GetAllMeasurementsBySsnPaginated function");

        try
        {
            var result = await _measurementService.GetAllMeasurementsBySsnPaginated(ssn, dto.PageNumber, dto.PageSize);

            Response.Headers.Add("X-Total-Count", result.TotalCount.ToString());

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }

    [HttpGet]
    [Route("GetMeasurement/{id}")]
    public async Task<IActionResult> GetMeasurementById([FromRoute] int id)
    {
        try
        {
            return Ok(await _measurementService.GetMeasurementById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost]
    [Route("CreateMeasurement")]
    public async Task<IActionResult> CreateMeasurement([FromBody] CreateMeasurementDto measurement)
    {
        using var activity = _tracer.StartActiveSpan("CreateMeasurement");

        LoggingService.Log.Information("Called CreateMeasurement function");
        try
        {
            return StatusCode(201, await _measurementService.CreateMeasurement(measurement));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("DeleteMeasurement/{id}")]
    public async Task<IActionResult> DeleteMeasurement([FromRoute] int id)
    {
        using var activity = _tracer.StartActiveSpan("DeleteMeasurement");

        LoggingService.Log.Information("Called DeleteMeasurement function");
        try
        {
            return Ok(await _measurementService.DeleteMeasurement(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }
    
    [HttpPut]
    [Route("UpdateMeasurement/{id}")]
    public async Task<IActionResult> UpdateMeasurement([FromRoute] int id, [FromBody] UpdateMeasurementDto dto)
    {
        using var activity = _tracer.StartActiveSpan("UpdateMeasurement");

        LoggingService.Log.Information("Called UpdateMeasurement function");

        try
        {
            return Ok(await _measurementService.UpdateMeasurement(id, dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [HttpDelete]
    [Route("DeleteMeasurements/{ssn}")]
    public async Task<IActionResult> DeleteMeasurementsOnPatient([FromRoute] string ssn)
    {
        try
        {
            await _measurementService.DeleteMeasurementsOnPatient(ssn);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
            await _measurementService.RebuildDatabase();
            return StatusCode(200, "Database recreated");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}