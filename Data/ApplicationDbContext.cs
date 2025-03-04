using DMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace DMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Inventory> Inventory { get; set; }

        public DbSet<PurchaseLocation> PurchaseLocations { get; set; }

        public DbSet<FuelType> FuelTypes { get; set; }

        public DbSet<PurchaseDeliveryNote> PurchaseDeliveryNotes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseLocation>().HasData(
                new PurchaseLocation { Id = 1, Name = "Showroom A" },
                new PurchaseLocation { Id = 2, Name = "Dealer X" },
                new PurchaseLocation { Id = 3, Name = "Warehouse Y" }
            );

        }
    }
}