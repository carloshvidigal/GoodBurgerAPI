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
}