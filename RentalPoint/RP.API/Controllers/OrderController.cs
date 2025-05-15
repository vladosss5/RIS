using Microsoft.AspNetCore.Mvc;
using RP.API.Dtos.Inventory;
using RP.API.Dtos.Order;
using RP.API.Mapping;
using RP.Application.Interfaces;

namespace RP.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly OrderMapper _mapper;

    public OrdersController(
        IOrderService orderService,
        OrderMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponseDto>> CreateOrder([FromBody] OrderCreateDto dto)
    {
        var order = _mapper.MapToDomain(dto);
        var createdOrder = await _orderService.CreateOrderAsync(order);
        var resultDto = _mapper.MapToDto(createdOrder);
        
        return CreatedAtAction(nameof(GetOrder), new { id = resultDto.Id }, resultDto);
    }

    [HttpPost("{orderId}/return")]
    public async Task<IActionResult> ReturnInventory(string orderId, [FromBody] ReturnInventoryDto dto)
    {
        await _orderService.ReturnInventoryAsync(orderId, dto.InventoryId);
        return NoContent();
    }
    
    /// <summary>
    /// Получить заказ по ID
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <response code="200">Заказ найден</response>
    /// <response code="404">Заказ не найден</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetOrder(string id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }
    
        var dto = _mapper.MapToDto(order);
        return Ok(dto);
    }
}