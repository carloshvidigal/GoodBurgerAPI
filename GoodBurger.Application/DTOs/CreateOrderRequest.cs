namespace GoodBurger.Application.DTOs;

public class CreateOrderRequest
{
    public int Sandwich { get; set; }
    public bool HasFries { get; set; }
    public bool HasDrink { get; set; }
}