namespace MeasurementService.Core.Entities;

public class Measurement
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public string SSN { get; set; }
    public bool ViewedByDoctor { get; set; }
}