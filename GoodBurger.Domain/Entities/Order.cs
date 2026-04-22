using GoodBurger.Domain.Enums;

namespace GoodBurger.Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public SandwichType Sandwich { get; set; }
    public bool HasFries { get; set; }
    public bool HasDrink { get; set; }
    public decimal Subtotal { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Total { get; private set; }

    public void Validate()
    {
        if (!Enum.IsDefined(typeof(SandwichType), Sandwich))
            throw new ArgumentException("Invalid sandwich type");
    }

    public void Calculate()
    {
        Validate();

        var subtotal = CalculateSubtotal();
        var discount = CalculateDiscount(subtotal);

        Subtotal = subtotal;
        Discount = discount;
        Total = subtotal - discount;
    }

    private decimal CalculateSubtotal()
    {
        decimal total = GetSandwichPrice();

        if (HasFries)
            total += ValueObjects.MenuPrices.Fries;

        if (HasDrink)
            total += ValueObjects.MenuPrices.Drink;

        return total;
    }

    private decimal GetSandwichPrice()
    {
        return Sandwich switch
        {
            SandwichType.XBurger => ValueObjects.MenuPrices.XBurger,
            SandwichType.XEgg => ValueObjects.MenuPrices.XEgg,
            SandwichType.XBacon => ValueObjects.MenuPrices.XBacon,
            _ => throw new ArgumentException("Invalid sandwich")
        };
    }

    private decimal CalculateDiscount(decimal subtotal)
    {
        if (HasFries && HasDrink)
            return subtotal * 0.20m;

        if (HasDrink && !HasFries)
            return subtotal * 0.15m;

        if (HasFries && !HasDrink)
            return subtotal * 0.10m;

        return 0;
    }
}