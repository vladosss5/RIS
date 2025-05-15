using Core.DBModels;
using Data;
using Microsoft.AspNetCore.Mvc;
using RP.API.Dtos.Client;

namespace RP.API.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly DbContext _context;

    public ClientsController(DbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Client>> Create([FromBody] ClientCreateDto dto)
    {
        var client = new Client
        {
            FName = dto.FName,
            SName = dto.SName,
            LName = dto.LName,
            PhoneNumber = dto.PhoneNumber
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetById(string id)
    {
        var client = await _context.Clients.FindAsync(id);
        return client == null ? NotFound() : Ok(client);
    }
}