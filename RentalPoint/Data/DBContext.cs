using Core.DBModels;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DBContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Deposit> Deposits { get; set; }
    public DbSet<Dictionary> Dictionaries { get; set; }
    public DbSet<DictionaryValue> DictionaryValues { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DBContext() { }
    
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Client entity
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.FName).IsRequired();
            entity.Property(e => e.SName).IsRequired();
            entity.Property(e => e.PhoneNumber).IsRequired();
        });

        // Configure Deposit entity
        modelBuilder.Entity<Deposit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.DocumentNumber).IsRequired();
                
            entity.HasOne(d => d.Type)
                .WithMany()
                .HasForeignKey(d => d.Type.Id)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Dictionary entity
        modelBuilder.Entity<Dictionary>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Type).IsRequired();
        });

        // Configure DictionaryValue entity
        modelBuilder.Entity<DictionaryValue>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Value).IsRequired();
                
            entity.HasOne(dv => dv.Dictionary)
                .WithMany(d => d.DictionaryValues)
                .HasForeignKey(dv => dv.Dictionary.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Inventory entity
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.PricePerHour).HasColumnType("decimal(18,2)");
                
            entity.HasOne(i => i.Type)
                .WithMany()
                .HasForeignKey(i => i.Type.Id)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Order entity
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
            entity.HasOne(o => o.Client)
                .WithMany(c => c.OrderLinks)
                .HasForeignKey(o => o.Client.Id)
                .OnDelete(DeleteBehavior.SetNull);
                
            entity.HasOne(o => o.Status)
                .WithMany()
                .HasForeignKey(o => o.Status.Id)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(o => o.Deposit)
                .WithMany(d => d.OrderLinks)
                .HasForeignKey(o => o.Deposit.Id)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}