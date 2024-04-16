using System.ComponentModel.DataAnnotations;

namespace MeasurementService.Core.Services.DTOs;

public class CreateMeasurementDto
{
    public DateTime Date { get; set; } = DateTime.Now;
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    [MinLength(10)] [MaxLength(10)] public string SSN { get; set; }
    public bool ViewedByDoctor { get; set; }
}