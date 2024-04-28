using FeatureHub;
using Microsoft.AspNetCore.Http;
using OpenTelemetry.Trace;

namespace PatientService.Test;

public class PatientApiTest
{

    [Fact]
    public async Task TestGetPatients_ReturnsListOfPatients()
    {
        // Arrange  
        var patients = new List<Patient>
        {
            new Patient
            {
                Email = "test@mail.dk",
                Name = "Test bruger 1",
                SSN = "1122334455"
            },

            new Patient
            {
                Email = "test@mail.dk",
                Name = "Test bruger 2",
                SSN = "1122334455"
            },
        };

        var mockService = new Mock<IPatientService>();
        mockService.Setup(service => service.GetAllPatients()).ReturnsAsync(patients);

        var tracer = TracerProvider.Default.GetTracer("serviceName");

        var featureHub = FeatureHubFactory.CreateFeatureHub();

        var controller = new PatientController(mockService.Object, featureHub, tracer);

        // Act  
        var result = await controller.GetAllPatients();

        // Assert  
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPosts = Assert.IsType<List<Patient>>(okResult.Value);
        Assert.Equal(patients.Count, returnedPosts.Count);
        for (int i = 0; i < patients.Count; i++)
        {
            Assert.Equal(patients[i].Email, returnedPosts[i].Email);
            Assert.Equal(patients[i].Name, returnedPosts[i].Name);
            Assert.Equal(patients[i].SSN, returnedPosts[i].SSN);
        }
    }
}