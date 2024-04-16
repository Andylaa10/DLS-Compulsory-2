using System.ComponentModel.DataAnnotations;

namespace MeasurementService.Core.Entities;

public class Measurement
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    [MinLength(10)] [MaxLength(10)] public string SSN { get; set; }
    public bool ViewedByDoctor { get; set; }
}