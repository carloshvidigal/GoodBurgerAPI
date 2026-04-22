using GoodBurger.Application.DTOs;
using GoodBurger.Application.Services;
using GoodBurger.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace GoodBurger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderResponse>> Create([FromBody] CreateOrderRequest request)
    {
        var result = await _orderService.CreateOrder(request);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<OrderResponse>>> GetAll()
    {
        var result = await _orderService.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderResponse>> GetById(Guid id)
    {
        var result = await _orderService.GetById(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderResponse>> Update(Guid id, [FromBody] CreateOrderRequest request)
    {
        var result = await _orderService.Update(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _orderService.Delete(id);
        return NoContent();
    }

    [HttpGet("menu")]
    [ProducesResponseType(typeof(MenuResponse), StatusCodes.Status200OK)]
    public ActionResult<MenuResponse> GetMenu()
    {
        var response = new MenuResponse
        {
            Sandwiches = new List<ItemDto>
            {
                new() { Name = "X Burger", Price = MenuPrices.XBurger },
                new() { Name = "X Egg", Price = MenuPrices.XEgg },
                new() { Name = "X Bacon", Price = MenuPrices.XBacon }
            },
            Extras = new List<ItemDto>
            {
                new() { Name = "Fries", Price = MenuPrices.Fries },
                new() { Name = "Drink", Price = MenuPrices.Drink }
            }
        };

        return Ok(response);
    }
}