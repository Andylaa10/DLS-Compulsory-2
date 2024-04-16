using MeasurementService.Core.DTOs;
using MeasurementService.Core.Entities;
using MeasurementService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeasurementService.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementController : ControllerBase
{

    private readonly IMeasurementService _measurementService;

    public MeasurementController(IMeasurementService measurementService)
    {
        _measurementService = measurementService;
    }
    
      
    [HttpGet]
    [Route("/{ssn}")]
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
    public async Task<IActionResult> CreateMeasurement([FromBody] Measurement measurement)
    {
        try
        {
            await _measurementService.CreateMeasurement(measurement);
            return StatusCode(201, "Successfully added measurement to db");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    [Route("DeleteMeasurement/{{id}}")]
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
    [Route("UpdateMeasurement/{{id}}")]
    public async Task<IActionResult> UpdateMeasurement([FromRoute] int id, [FromBody] UpdateMeasurementDTO dto)
    {
        try
        {
            await _measurementService.UpdateMeasurement(dto, id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }
}