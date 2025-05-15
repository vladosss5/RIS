using Core.DBModels;
using Data;
using Microsoft.EntityFrameworkCore;
using RP.Application.Interfaces;

namespace RP.Application.Services;

public class InventoryService : IInventoryService
{
    private readonly DataContext _context;

    public InventoryService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получить весь инвентарь
    /// </summary>
    public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
    {
        return await _context.Inventories
            .Include(i => i.Type)
            .Include(i => i.Status)
            .ToListAsync();
    }

    /// <summary>
    /// Создать новый инвентарь
    /// </summary>
    /// <param name="dto">DTO с данными инвентаря</param>
    public async Task<Inventory> CreateInventoryAsync(Inventory dto)
    {
        var inventory = new Inventory
        {
            Name = dto.Name,
            PricePerHour = dto.PricePerHour,
            TypeId = dto.TypeId,
            StatusId = dto.StatusId
        };

        _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();

        return inventory;
    }
}