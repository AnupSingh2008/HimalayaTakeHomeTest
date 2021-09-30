using Himalaya.Contract.Enum;
using Himalaya.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Himalaya.Data.Interfaces
{
    public interface IOrderBookRepository
    {
        Task<OrderBook> CreateOrder(OrderBook orderBook);

        Task<OrderBook> UpdateOrder(OrderBook orderBook);

        Task<int> DeleteOrder(string coinPair, int orderId);

        Task<List<OrderBook>> GetMarketPrice(string coinpair, int size, SideType side);
    }
}
