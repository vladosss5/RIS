using Core.DBModels;
using Microsoft.EntityFrameworkCore;

namespace Data;

/// <summary>
///     Контекст БД.
/// </summary>
public class DataContext : DbContext
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    public DataContext() { }
    
    /// <summary>
    ///     Конструктор с опциями.
    /// </summary>
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    /// <inheritdoc cref="Client"/>
    public DbSet<Client> Clients { get; set; }
    
    /// <inheritdoc cref="Deposit"/>
    public DbSet<Deposit> Deposits { get; set; }
    
    /// <inheritdoc cref="Dictionary"/>
    public DbSet<Dictionary> Dictionaries { get; set; }
    
    /// <inheritdoc cref="DictionaryValue"/>
    public DbSet<DictionaryValue> DictionaryValues { get; set; }
    
    /// <inheritdoc cref="Inventory"/>
    public DbSet<Inventory> Inventories { get; set; }
    
    /// <inheritdoc cref="Order"/>
    public DbSet<Order> Orders { get; set; }
    
    /// <inheritdoc cref="OrderInventories"/>
    public DbSet<OrderInventories> OrderInventories { get; set; }
    
    /// <summary>
    ///     Конфигурация контекста для работы с БД.
    /// </summary>
    /// <param name="optionsBuilder"> Опции контекста. </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var basePath = currentDirectory.Replace(@"\RP.View\bin\Debug\net8.0", "");
        var dbPath = Path.Combine(basePath, "RP.Data", "db.sqlite");

        optionsBuilder.UseSqlite($"RP.Data Source={dbPath};");
    }

    /// <summary>
    ///     Конфигурация моделей.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Конфигурация Client
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.SName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
        });

        // Конфигурация Deposit
        modelBuilder.Entity<Deposit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DocumentNumber).IsRequired().HasMaxLength(50);
            
            entity.HasOne(d => d.Type)
                .WithMany()
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Конфигурация Dictionary
        modelBuilder.Entity<Dictionary>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Type).IsUnique();
        });

        // Конфигурация DictionaryValue
        modelBuilder.Entity<DictionaryValue>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).IsRequired().HasMaxLength(200);
            
            entity.HasOne(dv => dv.Dictionary)
                .WithMany(d => d.DictionaryValues)
                .HasForeignKey(dv => dv.DictionaryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация Inventory
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.PricePerHour).HasColumnType("decimal(18,2)");
            
            entity.HasOne(i => i.Type)
                .WithMany()
                .HasForeignKey(i => i.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(i => i.Status)
                .WithMany()
                .HasForeignKey(i => i.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Конфигурация Order
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.FullPrice).HasColumnType("decimal(18,2)");
            
            entity.HasOne(o => o.Client)
                .WithMany(c => c.OrderLinks)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.SetNull);
                
            entity.HasOne(o => o.Status)
                .WithMany()
                .HasForeignKey(o => o.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(o => o.Deposit)
                .WithMany(d => d.OrderLinks)
                .HasForeignKey(o => o.DepositId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Конфигурация OrderInventories (новая связующая таблица)
        modelBuilder.Entity<OrderInventories>(entity =>
        {
            entity.HasKey(oi => new { oi.OrderId, oi.InventoryId });
            
            entity.Property(oi => oi.ReturnDateTime).IsRequired();
            
            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderInventories)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(oi => oi.Inventory)
                .WithMany(i => i.OrderInventories)
                .HasForeignKey(oi => oi.InventoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}