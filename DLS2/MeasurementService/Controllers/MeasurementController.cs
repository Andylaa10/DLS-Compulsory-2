using MeasurementService.Core.Services.DTOs;
using MeasurementService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;

namespace MeasurementService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeasurementController : ControllerBase
{
    private readonly ILogger<MeasurementController> _logger;
    private Tracer _tracer;
    
    private readonly IMeasurementService _measurementService;

    public MeasurementController(IMeasurementService measurementService, ILogger<MeasurementController> logger, Tracer tracer)
    {
        _measurementService = measurementService;
        _logger = logger;
        _tracer = tracer;
    }


    [HttpGet]
    [Route("{ssn}")]
    public async Task<IActionResult> GetAllMeasurementsBySsn([FromRoute] string ssn)
    {
        using var activity = _tracer.StartActiveSpan("GetAllMeasurementsBySsn");
        
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
    public async Task<IActionResult> GetAllMeasurementsBySsnPaginated([FromRoute] string ssn, [FromQuery] PaginationRequestDto dto)
    {
        using var activity = _tracer.StartActiveSpan("GetMeasurementPage");

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


    [HttpPost]
    [Route("CreateMeasurement")]
    public async Task<IActionResult> CreateMeasurement([FromBody] CreateMeasurementDto measurement)
    {
        using var activity = _tracer.StartActiveSpan("CreateMeasurement");

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

        try
        {
            return Ok(await _measurementService.UpdateMeasurement(id, dto));
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
        using var activity = _tracer.StartActiveSpan("RebuildDb");

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