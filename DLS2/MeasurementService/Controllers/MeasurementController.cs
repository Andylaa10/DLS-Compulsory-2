using MeasurementService.Core.Services.DTOs;
using MeasurementService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeasurementService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeasurementController : ControllerBase
{
    private readonly ILogger<MeasurementController> _logger;

    private readonly IMeasurementService _measurementService;

    public MeasurementController(IMeasurementService measurementService, ILogger<MeasurementController> logger)
    {
        _measurementService = measurementService;
        _logger = logger;
    }


    [HttpGet]
    [Route("{ssn}")]
    public async Task<IActionResult> GetAllMeasurementsBySsn([FromRoute] string ssn)
    {
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