using Himalaya.Data.Contexts;
using Himalaya.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himalaya.Data.Factories
{
    public class OrderBookDbContextFactory : IOrderBookDbContextFactory
    {
        //Hard Coded but basically will pass from top. due to time constrain writing it here.
        string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OrderBook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public SqlConnection Connection()
        {
            return new SqlConnection(ConnectionString);
        }

        OrderBookDbContext IOrderBookDbContextFactory.Create()
        {
            return new OrderBookDbContext(GetOptions());
        }

        private DbContextOptions<OrderBookDbContext> GetOptions()
        {
            var builder = new DbContextOptionsBuilder<OrderBookDbContext>();

            builder.UseSqlServer(ConnectionString);
            return builder.Options;
        }
    }
}
