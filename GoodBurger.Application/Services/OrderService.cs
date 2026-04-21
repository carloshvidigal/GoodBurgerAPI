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

    public List<OrderResponse> GetAll()
    {
        return _context.Orders.Select(order => new OrderResponse
            {
                Id = order.Id,
                Subtotal = order.Subtotal,
                Discount = order.Discount,
                Total = order.Total
            })
            .ToList();
    }

    public OrderResponse GetById(Guid id)
    {
        var order = _context.Orders.FirstOrDefault(x => x.Id == id);

        if (order == null)
            throw new Exception("Order not found");

        return new OrderResponse
        {
            Id = order.Id,
            Subtotal = order.Subtotal,
            Discount = order.Discount,
            Total = order.Total
        };
    }

    public OrderResponse Update(Guid id, CreateOrderRequest request)
    {
        var order = _context.Orders.FirstOrDefault(x => x.Id == id);

        if (order == null)
            throw new Exception("Order not found");

        order.Sandwich = (SandwichType)request.Sandwich;
        order.HasFries = request.HasFries;
        order.HasDrink = request.HasDrink;

        order.Calculate();

        _context.SaveChanges();

        return new OrderResponse
        {
            Id = order.Id,
            Subtotal = order.Subtotal,
            Discount = order.Discount,
            Total = order.Total
        };
    }

    public void Delete(Guid id)
    {
        var order = _context.Orders.FirstOrDefault(x => x.Id == id);

        if (order == null)
            throw new Exception("Order not found");

        _context.Orders.Remove(order);
        _context.SaveChanges();
    }
}