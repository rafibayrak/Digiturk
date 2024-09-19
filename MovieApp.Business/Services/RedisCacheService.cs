using System;
using System.Threading.Tasks;
using MovieApp.Business.Services.IServices;
using StackExchange.Redis;

namespace MovieApp.Business.Services
{
	public class RedisCacheService: IRedisCacheService
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDatabase _cache;

        public RedisCacheService(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            _cache = redisConnection.GetDatabase();
        }

        public async Task Clear(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }

        public void ClearAll()
        {
            var redisEndpoints = _redisConnection.GetEndPoints(true);
            foreach (var redisEndpoint in redisEndpoints)
            {
                var redisServer = _redisConnection.GetServer(redisEndpoint);
                redisServer.FlushAllDatabases();
            }
        }

        public string GetValue(string key)
        {
            return _cache.StringGet(key);
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await _cache.StringGetAsync(key);
        }

        public bool SetValue(string key, string value, TimeSpan timeSpan)
        {
            return _cache.StringSet(key, value, timeSpan);
        }

        public async Task<bool> SetValueAsync(string key, string value, TimeSpan timeSpan)
        {
            return await _cache.StringSetAsync(key, value, timeSpan);
        }
    }
}

