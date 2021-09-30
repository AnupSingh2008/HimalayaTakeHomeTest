using Himalaya.Contract.Enum;
using Himalaya.Data.Entities;
using Himalaya.Data.Factories;
using Himalaya.Data.Interfaces;
using Himalaya.Data.Repositories;
using Himalaya.TestHelper.Data;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Himalaya.Integration.Tests
{
    [TestFixture]
    public class OrderBookRepositoryTests
    {
        private IOrderBookRepository _orderBookRepository;
        private OrderBookDataService _orderBookDataService;

        [SetUp]
        public void SetUp()
        {
            _orderBookRepository = new OrderBookRepository(new OrderBookDbContextFactory());
            _orderBookDataService = new OrderBookDataService(new OrderBookDbContextFactory());
        }

        [TearDown]
        public void TearDown()
        {
            //_orderBookDataService.ClearData();
        }

        [Test]
        public async Task CreateOrderBook_PassedNull_ReturnedException()
        {
            // Arrange
            OrderBook orderBook = null;

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _orderBookRepository.CreateOrder(orderBook));
        }

        [Test]
        public async Task CreateOrderBook_PassedCorrectValue_ReturnExpectedValue() 
        {
           // Arrange
            var orderBook = new OrderBook { CoinPair = "BTCUSD", Price = 30000, Amount = 200, Side = SideType.Ask };

            //Act
            var entity = await _orderBookRepository.CreateOrder(orderBook);

            //Assert
            Assert.AreEqual(entity.CoinPair, orderBook.CoinPair);
            Assert.AreEqual(entity.Amount, orderBook.Amount);
            Assert.AreEqual(entity.Price, orderBook.Price);
            Assert.AreEqual(entity.Side, orderBook.Side);
        }

        [Test]
        public async Task UpdateOrderBook_PassedWrongIdValue_ReturnedExceptionWithExpectedErrorMessage()
        {
            // Arrange
            OrderBook orderBook = new OrderBook { Id = 9999 };

            //Act
            //Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _orderBookRepository.UpdateOrder(orderBook));

            Assert.AreEqual("Invalid data.", ex.Message);
        }

        [Test]
        public async Task UpdateOrderBook_PassedCorrectValue_ReturnExpectedValue()
        {
            // Arrange
            var newBook = new OrderBook { CoinPair = "XXXXXX", Price = 30030, Amount = 200, Side = SideType.Ask };
            var updateBook = new OrderBook { Id = newBook.Id, CoinPair = "yyyyyy", Price = 50000, Amount = 300, Side = SideType.Bid };


            //Act
            var newOrder = await _orderBookRepository.CreateOrder(newBook);
            updateBook.Id = newOrder.Id;
            var entity = await _orderBookRepository.UpdateOrder(updateBook);

            //Assert
            Assert.AreEqual(entity.CoinPair, updateBook.CoinPair);
            Assert.AreEqual(entity.Amount, updateBook.Amount);
            Assert.AreEqual(entity.Price, updateBook.Price);
            Assert.AreEqual(entity.Side, updateBook.Side);
        }

        [Test]
        public async Task DeleteOrderBook_PassedWrongIdValue_ReturnedExceptionWithExpectedErrorMessage()
        {
            // Arrange
            string coinpair = "BTCUSD";
            int orderID = 9999;

            //Act
            var ex = Assert.ThrowsAsync<Exception>(async () => await _orderBookRepository.DeleteOrder(coinpair, orderID));

            //Assert
            Assert.AreEqual("Invalid data.", ex.Message);
        }

        [Test]
        public async Task DeleteOrderBook_PassedCorrectValue_ReturnExpectedValue()
        {
            // Arrange
            var orderBook = new OrderBook { CoinPair = "asjfhsaj", Price = 306465, Amount = 300, Side = SideType.Ask };


            //Act
            var entity = await _orderBookRepository.CreateOrder(orderBook);
            var expectedorderId = await _orderBookRepository.DeleteOrder(entity.CoinPair, entity.Id);

            //Assert
            Assert.AreEqual(expectedorderId, entity.Id);
        }
    }
}
