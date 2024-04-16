using Microsoft.EntityFrameworkCore;
using PatientService.Core.Entities;

namespace PatientService.Helper;

public class PatientDbContext : DbContext
{
    public PatientDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //TODO FIX THIS CONNECTION STRING
        optionsBuilder.UseSqlServer("Server=patient-db;Database=PatientDb;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;");        
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Setup DB
        //Auto generate id
        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.SSN)
            .IsUnique();
        
        #endregion
    }
    
    public DbSet<Patient> Patients { get; set; }
}