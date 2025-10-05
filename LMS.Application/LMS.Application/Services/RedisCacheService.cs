using LMS.Application.Services.IServices;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace LMS.Application.Services
{
    // RedisCacheService.cs
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _db;
        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var data = await _db.StringGetAsync(key);
            if (data.IsNullOrEmpty)
                return default(T);
            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var json = JsonConvert.SerializeObject(value);
            await _db.StringSetAsync(key, json, expiration);
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }
    }

}
