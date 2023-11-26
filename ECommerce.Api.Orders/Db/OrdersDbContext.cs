using ECommerce.Api.Orders.Db;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Db
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrdersDbContext(DbContextOptions options) : base(options) 
        {

        }
    }
}
