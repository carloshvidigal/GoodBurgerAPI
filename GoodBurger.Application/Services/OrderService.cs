using GoodBurger.Application.DTOs;
using GoodBurger.Domain.Entities;
using GoodBurger.Domain.Enums;

namespace GoodBurger.Application.Services;

public class OrderService
{
    public OrderResponse CreateOrder(CreateOrderRequest request)
    {
        var order = new Order
        {
            Sandwich = (SandwichType)request.Sandwich,
            HasFries = request.HasFries,
            HasDrink = request.HasDrink
        };

        order.Calculate();

        return new OrderResponse
        {
            Id = order.Id,
            Subtotal = order.Subtotal,
            Discount = order.Discount,
            Total = order.Total
        };
    }
}