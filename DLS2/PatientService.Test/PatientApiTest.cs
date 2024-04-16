namespace PatientService.Test;

public class PatientApiTest
{
    [Fact]
    public async Task TestCreateMeasurement_ReturnsOkayAndPost()  
    {
        // Arrange    
        var dto = new CreatePatientDto()
        {  
            Email = "PatientMail@mail.com",
            SSN = "2201960000",
            Name = "Kristian Hansen Hollænder"
        };

        var expectedPatient = new Patient()
        {
            Id = 1,
            Email = "PatientMail@mail.com",
            SSN = "2201960000",
            Name = "Kristian Hansen Hollænder"
        };
        
        var mockService = new Mock<IPatientService>();  
        mockService.Setup(service => service.CreatePatient(It.IsAny<CreatePatientDto>()))  
            .ReturnsAsync(expectedPatient);

        var controller = new PatientController(mockService.Object);  
  
        // Act    
        var result = await controller.AddPatient(dto);  
  
        // Assert    
        var objectResult = Assert.IsType<ObjectResult>(result);
        var patientResult = Assert.IsType<Patient>(objectResult.Value);
        Assert.Equal(201, objectResult.StatusCode);
        Assert.Equal(expectedPatient, patientResult);
    }
    
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
                SSN  = "1122334455"  
            }, 
            
            new Patient  
            {  
                Email = "test@mail.dk",
                Name = "Test bruger 2",
                SSN  = "1122334455"
            },  
        };  
  
        var mockService = new Mock<IPatientService>();  
        mockService.Setup(service => service.GetAllPatients()).ReturnsAsync(patients);  
  
        var controller = new PatientController(mockService.Object);  
  
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