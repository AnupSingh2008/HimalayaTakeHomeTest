using Himalaya.Contract;
using Himalaya.Contract.Enum;
using Himalaya.Data.Contexts;
using Himalaya.Data.Entities;
using Himalaya.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himalaya.Data.Repositories
{
    public class OrderBookRepository : IOrderBookRepository
    {
       
        private readonly IOrderBookDbContextFactory _contextFactory;

        public OrderBookRepository(IOrderBookDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;

        }
        public  async Task<OrderBook> CreateOrder(OrderBook orderBook)
        {
            if (orderBook == null)
            {
                throw new ArgumentNullException(nameof(orderBook));
            }
            using (var context = _contextFactory.Create())
            {
                await context.OrderBooks.AddAsync(orderBook);
                await context.SaveChangesAsync();
            }

            return new OrderBook
            {
                CoinPair = orderBook.CoinPair,
                Amount = orderBook.Amount,
                Price = orderBook.Price,
                Side = orderBook.Side,
                Id = orderBook.Id
            };
        }


        public async Task<OrderBook> UpdateOrder(OrderBook orderBook)
        {
            using(var context = _contextFactory.Create())
            {
                var entity = await context.OrderBooks.FirstOrDefaultAsync(o => o.Id == orderBook.Id);
                if(entity == null)
                {
                    throw new Exception("Invalid data.");
                }

                entity.CoinPair = orderBook.CoinPair;
                entity.Amount = orderBook.Amount;
                entity.Price = orderBook.Price;
                entity.Side = orderBook.Side;

                await context.SaveChangesAsync();
                return new OrderBook
                {
                    CoinPair = orderBook.CoinPair,
                    Amount = orderBook.Amount,
                    Price = orderBook.Price,
                    Side = orderBook.Side,
                    Id= orderBook.Id
                };
            }
        }
        public async Task<List<OrderBook>> GetMarketPrice(string coinpair, int size, SideType side)
        {
            using(var context = _contextFactory.Create())
            {
                var orderBook = await context.OrderBooks.Where(O=>O.CoinPair == coinpair && O.Price == size && O.Side == side).ToListAsync();
                return orderBook;
            }
        }

        

        public async Task<int> DeleteOrder(string coinPair, int orderId)
        {
            using (var context = _contextFactory.Create())
            {
                var entity = await context.OrderBooks.FirstOrDefaultAsync(o => o.Id == orderId);
                if (entity == null)
                {
                    throw new Exception("Invalid data.");
                }

                context.Remove(entity);
                context.SaveChanges();
            }
            return orderId;
        }
    }
}
