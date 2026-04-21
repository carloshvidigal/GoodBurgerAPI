using Microsoft.EntityFrameworkCore;
using GoodBurger.Domain.Entities;

namespace GoodBurger.Infrastructure;

public class GoodBurgerDbContext : DbContext
{
    public GoodBurgerDbContext(DbContextOptions<GoodBurgerDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
}