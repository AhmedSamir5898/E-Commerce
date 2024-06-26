﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.IRepository;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket ?> GetBasketAsync(string id)
        {
           var basket= await _database.StringGetAsync(id);
           return  basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }
        public async Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket basket)
        {
            var createdorDeleted = await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket) ,TimeSpan.FromDays(20));
            if (createdorDeleted is false) return null;
           return await GetBasketAsync(basket.Id);

        }
    }
}
