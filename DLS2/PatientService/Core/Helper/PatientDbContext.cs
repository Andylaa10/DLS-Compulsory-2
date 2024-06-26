﻿using Microsoft.EntityFrameworkCore;
using PatientService.Core.Entities;

namespace PatientService.Core.Helper;

public class PatientDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=patient-db;Database=PatientDb;User Id=SA;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;");        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Setup DB
        //Auto generate id
        modelBuilder.Entity<Patient>().HasKey(p => p.SSN);

        modelBuilder.Entity<Patient>().HasIndex(p => p.SSN).IsUnique();
        modelBuilder.Entity<Patient>().HasIndex(p => p.Email).IsUnique();
        #endregion
    }

    public DbSet<Patient> Patients { get; set; }
}