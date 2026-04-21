using GoodBurger.Application.DTOs;
using GoodBurger.Domain.Entities;
using GoodBurger.Domain.Enums;
using GoodBurger.Infrastructure;


namespace GoodBurger.Application.Services;

public class OrderService
{
    private readonly GoodBurgerDbContext _context;

    public OrderService(GoodBurgerDbContext context)
    {
        _context = context;
    }
    public OrderResponse CreateOrder(CreateOrderRequest request)
    {
        var order = new Order
        {
            Sandwich = (SandwichType)request.Sandwich,
            HasFries = request.HasFries,
            HasDrink = request.HasDrink
        };

        order.Calculate();

        _context.Orders.Add(order);
        _context.SaveChanges();

        return new OrderResponse
        {
            Id = order.Id,
            Subtotal = order.Subtotal,
            Discount = order.Discount,
            Total = order.Total
        };
    }
}