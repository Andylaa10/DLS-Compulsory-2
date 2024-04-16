using System.Diagnostics.Metrics;
using System.Security.Claims;
using MeasurementService.Controllers;
using MeasurementService.Core.Entities;
using MeasurementService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MeasurementService.Test;

public class MeasurementApiTest
{
    [Fact]
    public async Task TestCreateMeasurement_ReturnsOkayAndPost()  
    {
        // Arrange    
        var testMeasurement = new Measurement
        {  
            Id = 1,
            Date  = DateTime.UtcNow,
            Diastolic  = 3,
            Systolic  = 4,
            SSN = "2201960000",
            ViewedByDoctor = true
        };  
        
        var mockService = new Mock<IMeasurementService>();  
        mockService.Setup(service => service.CreateMeasurement(It.IsAny<Measurement>()))  
            .ReturnsAsync(testMeasurement);

        var controller = new MeasurementController(mockService.Object);  
  
        // Act    
        var result = await controller.CreateMeasurement(testMeasurement);  
  
        // Assert    
        var objectResult = Assert.IsType<ObjectResult>(result);
        var postResult = Assert.IsType<Measurement>(objectResult.Value);
        Assert.Equal(201, objectResult.StatusCode);
        Assert.Equal(testMeasurement, postResult);
    }
}