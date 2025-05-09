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
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var basePath = currentDirectory.Replace(@"\RentalPoint\bin\Debug\net8.0", "");
        var dbPath = Path.Combine(basePath, "Data", "db.sqlite");

        optionsBuilder.UseSqlite($"Data Source={dbPath};");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Клиент
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FName).IsRequired();
            entity.Property(e => e.SName).IsRequired();
            entity.Property(e => e.PhoneNumber).IsRequired();
        });

        // Депозит
        modelBuilder.Entity<Deposit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DocumentNumber).IsRequired();
                
            entity.HasOne(d => d.Type)
                .WithMany()
                .HasForeignKey(d => d.TypeId) // Используем явный внешний ключ
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Справочник
        modelBuilder.Entity<Dictionary>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type).IsRequired();
        });

        // Значение справочника
        modelBuilder.Entity<DictionaryValue>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).IsRequired();
                
            entity.HasOne(dv => dv.Dictionary)
                .WithMany(d => d.DictionaryValues)
                .HasForeignKey(dv => dv.DictionaryId) // Добавьте DictionaryId в DictionaryValue
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Инвентарь
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.PricePerHour).HasColumnType("decimal(18,2)");
                
            entity.HasOne(i => i.Type)
                .WithMany()
                .HasForeignKey(i => i.TypeId) // Используем явный внешний ключ
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Заказ
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
                
            entity.HasOne(o => o.Client)
                .WithMany(c => c.OrderLinks)
                .HasForeignKey(o => o.ClientId) // Используем явный внешний ключ
                .OnDelete(DeleteBehavior.SetNull);
                
            entity.HasOne(o => o.Status)
                .WithMany()
                .HasForeignKey(o => o.StatusId) // Используем явный внешний ключ
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(o => o.Deposit)
                .WithMany(d => d.OrderLinks)
                .HasForeignKey(o => o.DepositId) // Используем явный внешний ключ
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}