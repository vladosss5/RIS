using Core.DBModels;
using Data;
using Microsoft.AspNetCore.Mvc;
using RP.API.Dtos.Client;
using RP.API.Mapping;
using RP.Application.Interfaces;

namespace RP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly ClientsMapper _mappingService;

    public ClientsController(
        IClientService clientService,
        ClientsMapper mappingService)
    {
        _clientService = clientService;
        _mappingService = mappingService;
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ClientDto>> CreateClient([FromBody] ClientCreateDto dto)
    {
        var client = _mappingService.MapToDomain(dto); // Явный вызов маппера
        var createdClient = await _clientService.CreateClientAsync(client);
        var resultDto = _mappingService.MapToDto(createdClient);
        
        return CreatedAtAction(nameof(GetClient), new { id = resultDto.Id }, resultDto);
    }
    
    /// <summary>
    /// Получить клиента по ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDto>> GetClient(string id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null)
        {
            return NotFound();
        }
    
        var dto = _mappingService.MapToDto(client);
        return Ok(dto);
    }
}