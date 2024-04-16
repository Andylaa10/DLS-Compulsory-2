using System.ComponentModel.DataAnnotations;

namespace PatientService.Core.Entities;

public class Patient
{
    [MinLength(10)] [MaxLength(10)] public string SSN { get; set; }

    [EmailAddress] public string Email { get; set; }

    public string Name { get; set; }
}