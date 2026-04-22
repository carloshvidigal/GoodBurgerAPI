using GoodBurger.Domain.Entities;
using GoodBurger.Domain.Enums;
using Xunit;

namespace GoodBurger.Tests;

public class OrderTests
{
    [Fact]
    public void Should_Calculate_20_Percent_Discount_When_Has_All_Items()
    {
        var order = new Order
        {
            Sandwich = SandwichType.XBurger,
            HasFries = true,
            HasDrink = true
        };

        order.Calculate();

        Assert.Equal(9.5m, order.Subtotal);
        Assert.Equal(1.9m, order.Discount);
        Assert.Equal(7.6m, order.Total);
    }

    [Fact]
    public void Should_Calculate_15_Percent_Discount_When_Has_Drink_Only()
    {
        var order = new Order
        {
            Sandwich = SandwichType.XBurger,
            HasFries = false,
            HasDrink = true
        };

        order.Calculate();

        Assert.Equal(7.5m, order.Subtotal);
        Assert.Equal(1.125m, order.Discount);
        Assert.Equal(6.375m, order.Total);
    }

    [Fact]
    public void Should_Calculate_10_Percent_Discount_When_Has_Fries_Only()
    {
        var order = new Order
        {
            Sandwich = SandwichType.XBurger,
            HasFries = true,
            HasDrink = false
        };

        order.Calculate();

        Assert.Equal(7.0m, order.Subtotal);
        Assert.Equal(0.7m, order.Discount);
        Assert.Equal(6.3m, order.Total);
    }

    [Fact]
    public void Should_Not_Apply_Discount_When_No_Extras()
    {
        var order = new Order
        {
            Sandwich = SandwichType.XBurger,
            HasFries = false,
            HasDrink = false
        };

        order.Calculate();

        Assert.Equal(5.0m, order.Subtotal);
        Assert.Equal(0m, order.Discount);
        Assert.Equal(5.0m, order.Total);
    }

    [Fact]
    public void Should_Throw_Exception_When_Sandwich_Is_Invalid()
    {
        var order = new Order
        {
            Sandwich = (SandwichType)999
        };

        Assert.Throws<ArgumentException>(() => order.Calculate());
    }
}