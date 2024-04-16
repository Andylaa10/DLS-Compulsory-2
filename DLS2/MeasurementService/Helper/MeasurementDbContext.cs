using MeasurementService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeasurementService.Helper;

public class MeasurementDbContext : DbContext
{
    public MeasurementDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //TODO FIX THIS CONNECTION STRING
        optionsBuilder.UseSqlServer("Server=localhost;Database=MeasurementDb;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;");        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Setup DB
        //Auto generate id
        modelBuilder.Entity<Measurement>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd();
        
        #endregion
    }

    public DbSet<Measurement> Measurements { get; set; }

}