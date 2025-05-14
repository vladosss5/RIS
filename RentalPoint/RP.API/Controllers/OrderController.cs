using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RP.API.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    public OrderController()
    {
        
    }

    [HttpGet("table")]
    public async Task<IActionResult> GetAllOrders()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(string id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderById(string id)
    {
        return NoContent();
    }
}