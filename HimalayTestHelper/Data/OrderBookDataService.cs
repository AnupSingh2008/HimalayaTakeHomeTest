using Himalaya.Data.Entities;
using Himalaya.Data.Interfaces;
using System.Linq;

namespace Himalaya.TestHelper.Data
{
    public class OrderBookDataService
    {
        private readonly IOrderBookDbContextFactory _contextFactory;

        public OrderBookDataService(IOrderBookDbContextFactory orderBookDbContextFactory)
        {
            _contextFactory = orderBookDbContextFactory;

        }

        public OrderBook GetOrderBook(int id)
        {
            using(var context = _contextFactory.Create())
            {
                return context.OrderBooks.FirstOrDefault(o => o.Id == id);
            }
        }

        public void ClearData()
        {
            using(var context = _contextFactory.Create())
            {
                context.OrderBooks.RemoveRange(context.OrderBooks);
                context.SaveChanges();
            }
        }
    }
}
