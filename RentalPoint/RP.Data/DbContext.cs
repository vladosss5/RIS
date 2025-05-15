using Core.DBModels;
using Microsoft.EntityFrameworkCore;

namespace Data;

/// <summary>
///     Контекст БД.
/// </summary>
public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    /// <summary>
    ///     Конструктор.
    /// </summary>
    public DbContext() { }
    
    /// <summary>
    ///     Конструктор с опциями.
    /// </summary>
    public DbContext(DbContextOptions<DbContext> options) : base(options)
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
                .HasForeignKey(d => d.TypeId)
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
                .HasForeignKey(dv => dv.DictionaryId)
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
                .HasForeignKey(i => i.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(i => i.Status)
                .WithMany()
                .HasForeignKey(i => i.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Заказ
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
                
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
    }
}