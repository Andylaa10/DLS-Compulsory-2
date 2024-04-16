

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
    
    [Fact]
    public async Task TestEditMeasurement_ReturnsOkay()
    {
        // Arrange  
        var measurementId = 1;  
        var updateMeasurementDto = new UpdateMeasurementDTO()  
        {  
            ViewedByDoctor = true
        };
        
        var mockService = new Mock<IMeasurementService>();  
        mockService.Setup(service => service.UpdateMeasurement(measurementId, updateMeasurementDto)).Returns(Task.CompletedTask);  
        
        var controller = new MeasurementController(mockService.Object);
  
        // Act  
        var result = await controller.UpdateMeasurement(measurementId, updateMeasurementDto);  
  
        // Assert  
        var okResult = Assert.IsType<OkResult>(result);  
        Assert.Equal(200, okResult.StatusCode);  
    }
}