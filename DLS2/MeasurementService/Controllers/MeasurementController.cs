using MeasurementService.Core.Services.DTOs;
using MeasurementService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeasurementService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeasurementController : ControllerBase
{

    private readonly IMeasurementService _measurementService;

    public MeasurementController(IMeasurementService measurementService)
    {
        _measurementService = measurementService;
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
            await _measurementService.DeleteMeasurement(id);
            return Ok();
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
            await _measurementService.UpdateMeasurement(id, dto);
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
            await _measurementService.RebuildDatabase();
            return StatusCode(200, "Database recreated");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}