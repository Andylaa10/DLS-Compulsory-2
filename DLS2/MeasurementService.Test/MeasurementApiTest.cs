using System.Diagnostics.Metrics;
using OpenTelemetry.Trace;

namespace MeasurementService.Test;

public class MeasurementApiTest
{
    [Fact]
    public async Task TestCreateMeasurement_ReturnsOkayAndPost()  
    {
        // Arrange    
        var dto = new CreateMeasurementDto()
        {
            Date  = DateTime.Now,
            Diastolic  = 3,
            Systolic  = 4,
            SSN = "2201960000",
            ViewedByDoctor = true
        };
        
        var expectedMeasurement = new Measurement
        {  
            Id = 1,
            Date  = DateTime.Now,
            Diastolic  = 3,
            Systolic  = 4,
            SSN = "2201960000",
            ViewedByDoctor = true
        };  
        
        var mockService = new Mock<IMeasurementService>();  
        mockService.Setup(service => service.CreateMeasurement(It.IsAny<CreateMeasurementDto>()))  
            .ReturnsAsync(expectedMeasurement);

        var tracer = TracerProvider.Default.GetTracer("serviceName");

        var controller = new MeasurementController(mockService.Object, tracer);  
  
        // Act    
        var result = await controller.CreateMeasurement(dto);  
  
        // Assert    
        var objectResult = Assert.IsType<ObjectResult>(result);
        var measurementResult = Assert.IsType<Measurement>(objectResult.Value);
        Assert.Equal(201, objectResult.StatusCode);
        Assert.Equal(expectedMeasurement, measurementResult);
    }
    
    [Fact]
    public async Task TestEditMeasurement_ReturnsOkay()
    {
        // Arrange  
        var measurementId = 1;  
        var updateMeasurementDto = new UpdateMeasurementDto()  
        {  
            ViewedByDoctor = true
        };
    
        var mockService = new Mock<IMeasurementService>();  
        mockService.Setup(service => service.UpdateMeasurement(measurementId, updateMeasurementDto)).ReturnsAsync(new Measurement());

        var tracer = TracerProvider.Default.GetTracer("serviceName");
        
        var controller = new MeasurementController(mockService.Object, tracer);  

        // Act  
        var result = await controller.UpdateMeasurement(measurementId, updateMeasurementDto);  

        // Assert  
        var okResult = Assert.IsType<OkObjectResult>(result);  
        Assert.Equal(200, okResult.StatusCode);  
    }
}