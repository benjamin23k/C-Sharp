using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Patients;

namespace Database
{
    public class PatientDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=DbPatient;Trusted_Connection=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<Patient> Patient { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.Personal)
                .IsUnique();
        }
    }
}
