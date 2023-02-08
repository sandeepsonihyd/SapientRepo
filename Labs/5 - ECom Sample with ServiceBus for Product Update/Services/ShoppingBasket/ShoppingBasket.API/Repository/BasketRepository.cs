using Newtonsoft.Json;
using ShoppingBasket.API.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.API.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        public BasketRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string buyerId)
        {
            return await _database.KeyDeleteAsync(buyerId);
        }

        public async Task<Basket> GetBasketAsync(string buyerId)
        {
            var value = await _database.StringGetAsync(buyerId);
            if (value.IsNullOrEmpty)
                return null;
            Basket basket = JsonConvert.DeserializeObject<Basket>(value);
            return basket;
        }

        public IEnumerable<string> GetBuyers()
        {
            var endpoint = _redis.GetEndPoints();
            var server = _redis.GetServer(endpoint.First());
            var data = server.Keys();
            return data?.Select(k => k.ToString());
        }

        public async Task<Basket> UpdateBasketAsync(Basket basket)
        {
            await _database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));
            return await GetBasketAsync(basket.BuyerId);
        }
    }
}
