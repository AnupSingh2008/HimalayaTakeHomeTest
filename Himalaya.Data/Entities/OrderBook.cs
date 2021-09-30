using Himalaya.Contract.Enum;

namespace Himalaya.Data.Entities
{
    public class OrderBook
    {
        public int Id { get; set; }
        public string CoinPair { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public SideType Side { get; set; }
    }
}
