using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using RIS.Core.Models;

namespace RIS.Data;

public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<InventoryType> InventoryTypes { get; set; }
    public DbSet<Inventory> Inventory { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<RentalItem> RentalItems { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var basePath = currentDirectory.Replace(@"\RIS\bin\Debug\net9.0", "");
        var dbPath = Path.Combine(basePath, "RIS.Data", "db.sqlite");

        optionsBuilder.UseSqlite($"Data Source={dbPath};");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Автогенерация GUID для Id при добавлении в БД
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            modelBuilder.Entity(entityType.ClrType)
                .Property<string>("Id")
                .HasValueGenerator<GuidValueGenerator>();
        }

        // Связи
        modelBuilder.Entity<Inventory>()
            .HasOne(i => i.Type)
            .WithMany(t => t.Inventory)
            .HasForeignKey(i => i.TypeId);

        modelBuilder.Entity<Rental>()
            .HasMany(r => r.RentalItems)
            .WithOne(ri => ri.Rental)
            .HasForeignKey(ri => ri.RentalId);

        modelBuilder.Entity<Inventory>()
            .HasMany(i => i.RentalItems)
            .WithOne(ri => ri.Inventory)
            .HasForeignKey(ri => ri.InventoryId);
    }
}