using Himalaya.Contract.Enum;
using Himalaya.Data.Entities;
using Himalaya.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Himalaya.Api.Controllers
{
    public class OrderBookController : Controller
    {
        private readonly IOrderBookRepository _orderBookRepository;

        public OrderBookController(IOrderBookRepository orderBookRepository)
        {
            _orderBookRepository = orderBookRepository;
        }
        

        /// <summary>
        /// Get Market Price based on Coin Pair, Size and Bid
        /// </summary>
        /// <param name="coinpar"></param>
        /// <param name="size"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        [HttpGet("{coinpair}/{size}/{bid}", Name = "GetMarketPrice")]
        public async Task<ActionResult<IEnumerable<OrderBook>>> GetMarketPrice(string coinpar,int size, SideType bid)
        {
            Console.WriteLine("--> Getting Market Price....");

            var OrderBooks = await _orderBookRepository.GetMarketPrice(coinpar,size,bid);

            if (OrderBooks != null)
            {
                return Ok(OrderBooks);
            }

            return NotFound();
        }

        /// <summary>
        /// Update the order book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderBook"></param>
        /// <returns></returns>
        [HttpPut("{id}",Name ="UpdateOrderBook")]
        public async Task<ActionResult<OrderBook>> UpdateOrderBook(int id, OrderBook orderBook)
        {

            _orderBookRepository.UpdateOrder(orderBook);

            return NoContent();
        }

        /// <summary>
        /// Delete order book , pass Coin Paair and Order ID
        /// </summary>
        /// <param name="coinpair"></param>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [HttpDelete("{id}",Name ="DeleteOrderBook")]
        public async Task<ActionResult<OrderBook>> DeleteOrderBook(string coinpair,int orderid)
        {

            _orderBookRepository.DeleteOrder(coinpair,orderid);

            return NoContent();
        }
       

    }
}
