using Microsoft.AspNetCore.Mvc;
using RP.API.Dtos.Order;
using RP.API.Mapping;
using RP.Application.Interfaces;

namespace RP.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly OrderMapper _orderMapper;

    public OrdersController(
        IOrderService orderService, 
        OrderMapper orderMapper)
    {
        _orderService = orderService;
        _orderMapper = orderMapper;
    }

    /// <summary>
    /// Создать новый заказ на прокат
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<OrderResponseDto>> CreateOrder([FromBody] OrderCreateDto dto)
    {
        var order = _orderMapper.MapToDomain(dto);
        var createdOrder = await _orderService.CreateOrderAsync(order);
        var resultDto = _orderMapper.MapToDto(createdOrder);
        
        return CreatedAtAction(
            nameof(GetOrderById), 
            new { id = resultDto.Id }, 
            resultDto);
    }

    /// <summary>
    /// Получить заказ по ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetOrderById(string id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
            return NotFound();
            
        return _orderMapper.MapToDto(order);
    }

    /// <summary>
    /// Завершить заказ и рассчитать стоимость
    /// </summary>
    [HttpPatch("{id}/complete")]
    public async Task<ActionResult<OrderCompletionResultDto>> CompleteOrder(string id)
    {
        var result = await _orderService.CompleteOrderAsync(id);
        return Ok(result);
    }
}