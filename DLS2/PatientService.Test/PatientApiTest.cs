using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PatientService.Controllers;
using PatientService.Core.Entities;
using PatientService.Core.Services.Interfaces;

namespace PatientService.Test;

public class PatientApiTest
{
    [Fact]
    public async Task TestCreateMeasurement_ReturnsOkayAndPost()  
    {
        // Arrange    
        var testPatient = new Patient
        {  
            Email = "PatientMail@mail.com",
            SSN = "2201960000",
            Name = "Kristian Hansen Hollænder"
        };  
        
        var mockService = new Mock<IPatientService>();  
        mockService.Setup(service => service.CreatePatient(It.IsAny<Patient>()))  
            .ReturnsAsync(testPatient);

        var controller = new PatientController(mockService.Object);  
  
        // Act    
        var result = await controller.AddPatient(testPatient);  
  
        // Assert    
        var objectResult = Assert.IsType<ObjectResult>(result);
        var postResult = Assert.IsType<Patient>(objectResult.Value);
        Assert.Equal(201, objectResult.StatusCode);
        Assert.Equal(testPatient, postResult);
    }
}