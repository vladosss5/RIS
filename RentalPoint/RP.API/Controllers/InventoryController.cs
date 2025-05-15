using Core.DBModels;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RP.API.Dtos.Inventory;
using RP.API.Dtos.Satatuses;

namespace RP.API.Controllers;

[ApiController]
[Route("api/inventory")]
public class InventoryController : ControllerBase
{
    private readonly DataContext _context;

    public InventoryController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Inventory>>> GetAll()
    {
        return await _context.Inventories
            .Include(i => i.Type)
            .Include(i => i.Status)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Inventory>> GetById(string id)
    {
        var inventory = await _context.Inventories
            .Include(i => i.Type)
            .Include(i => i.Status)
            .FirstOrDefaultAsync(i => i.Id == id);

        return inventory == null ? NotFound() : Ok(inventory);
    }

    [HttpPost]
    public async Task<ActionResult<Inventory>> Create([FromBody] InventoryCreateDto dto)
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

        return CreatedAtAction(nameof(GetById), new { id = inventory.Id }, inventory);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(string id, [FromBody] StatusUpdateDto dto)
    {
        var inventory = await _context.Inventories.FindAsync(id);
        if (inventory == null) return NotFound();

        inventory.StatusId = dto.StatusId;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
