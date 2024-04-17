using MeasurementService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeasurementService.Core.Helper;

public class MeasurementDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=measurement-db;Database=MeasurementDb;User Id=SA;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;");        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Setup DB

        //Auto generate id
        modelBuilder.Entity<Measurement>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        #endregion }
    }

    public DbSet<Measurement> Measurements { get; set; }
}