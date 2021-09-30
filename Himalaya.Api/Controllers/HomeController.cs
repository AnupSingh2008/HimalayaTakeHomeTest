using Himalaya.Data.Entities;
using Himalaya.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Himalaya.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderBookRepository _orderBookRepository;

        public HomeController(IOrderBookRepository orderBookRepository)
        {
            _orderBookRepository = orderBookRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create()
        {

            return Ok();
        }

        /// <summary>
        /// To Create the Order
        /// </summary>
        /// <param name="orderBook"></param>
        /// <returns></returns>
        [HttpPost(Name = "CreateOrderBook")]
        public async Task<ActionResult<OrderBook>> CreateOrderBook([FromBody] OrderBook orderBook)
        {

            _orderBookRepository.CreateOrder(orderBook);

            return Ok();
        }

    }
}
