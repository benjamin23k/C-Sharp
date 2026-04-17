using System;
using System.Collections.Generic;
using System.Linq;
using Order.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;

namespace Database
{
    public class OrderDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string ConnectionString = "Server=DESKTOP-P2LDKRQ\\SQLEXPRESS;Database=OrderDB;Trusted_Connection=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<Orders> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>()
               .HasIndex(p => p.plate)
               .IsUnique(false);

            modelBuilder.Entity<Orders>()
                .Property(o => o.createdAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Orders>()
                .Property(o => o.updatedAt)
                .HasDefaultValueSql("GETDATE()");
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<Orders>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.createdAt = DateTime.Now;
                    entry.Entity.updatedAt = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.updatedAt = DateTime.Now;
                }
            }
        }
    }
}
