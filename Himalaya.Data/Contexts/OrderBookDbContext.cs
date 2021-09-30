using Himalaya.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Himalaya.Data.Contexts
{
    public class OrderBookDbContext:DbContext
    {
        public OrderBookDbContext(DbContextOptions<OrderBookDbContext> opt) : base(opt)
        {

        }

        public DbSet<OrderBook> OrderBooks { get; set; }
       
    }
}
