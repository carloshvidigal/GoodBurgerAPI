using Microsoft.AspNetCore.Mvc;
using GoodBurger.Application.Services;
using GoodBurger.Application.DTOs;

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
    public IActionResult Create(CreateOrderRequest request)
    {
        var result = _orderService.CreateOrder(request);
        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _orderService.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var result = _orderService.GetById(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, CreateOrderRequest request)
    {
        var result = _orderService.Update(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _orderService.Delete(id);
        return NoContent();
    }
}