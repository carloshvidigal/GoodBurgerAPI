using GoodBurger.Application.DTOs;
using GoodBurger.Domain.Entities;
using GoodBurger.Domain.Enums;
using GoodBurger.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GoodBurger.Application.Services;

public class OrderService
{
    private readonly GoodBurgerDbContext _context;

    public OrderService(GoodBurgerDbContext context)
    {
        _context = context;
    }

    public async Task<OrderResponse> CreateOrder(CreateOrderRequest request)
    {
        ValidateSandwich(request.Sandwich);

        var order = new Order
        {
            Sandwich = (SandwichType)request.Sandwich,
            HasFries = request.HasFries,
            HasDrink = request.HasDrink
        };

        order.Calculate();

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return MapToResponse(order);
    }

    public async Task<List<OrderResponse>> GetAll()
    {
        return await _context.Orders
            .Select(order => MapToResponse(order))
            .ToListAsync();
    }

    public async Task<OrderResponse> GetById(Guid id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

        if (order == null)
            throw new KeyNotFoundException("Order not found");

        return MapToResponse(order);
    }

    public async Task<OrderResponse> Update(Guid id, CreateOrderRequest request)
    {
        ValidateSandwich(request.Sandwich);

        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

        if (order == null)
            throw new KeyNotFoundException("Order not found");

        order.Sandwich = (SandwichType)request.Sandwich;
        order.HasFries = request.HasFries;
        order.HasDrink = request.HasDrink;

        order.Calculate();

        await _context.SaveChangesAsync();

        return MapToResponse(order);
    }

    public async Task Delete(Guid id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

        if (order == null)
            throw new KeyNotFoundException("Order not found");

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    // Helpers

    private void ValidateSandwich(int sandwich)
    {
        if (!Enum.IsDefined(typeof(SandwichType), sandwich))
            throw new ArgumentException("Invalid sandwich");
    }

    private static OrderResponse MapToResponse(Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            Subtotal = Math.Round(order.Subtotal, 2, MidpointRounding.AwayFromZero),
            Discount = Math.Round(order.Discount, 2, MidpointRounding.AwayFromZero),
            Total = Math.Round(order.Total, 2, MidpointRounding.AwayFromZero)
        };
    }
}