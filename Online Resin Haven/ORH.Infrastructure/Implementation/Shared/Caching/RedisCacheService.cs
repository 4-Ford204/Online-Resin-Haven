using Microsoft.Extensions.Caching.Distributed;
using ORH.Application.Interface.Shared.Caching;
using StackExchange.Redis;
using System.Text.Json;

namespace ORH.Infrastructure.Implementation.Shared.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<string?> GetAsync(string key)
        {
            var response = await _distributedCache.GetStringAsync(key);
            return response;
        }

        public Task SetAsync(string key, object value, TimeSpan? expiration = null)
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                return Task.CompletedTask;
            }

            var json = JsonSerializer.Serialize(value);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(5)
            };

            return _distributedCache.SetStringAsync(key, json, options);
        }

        public async Task<bool> RemoveAsync(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                await _distributedCache.RemoveAsync(key);
            }

            return true;
        }

        public async Task<bool> RemovePatternAsync(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                foreach (var endpoint in _connectionMultiplexer.GetEndPoints())
                {
                    var server = _connectionMultiplexer.GetServer(endpoint);

                    if (!server.IsConnected || server.IsReplica)
                    {
                        continue;
                    }

                    var keys = server.Keys(pattern: pattern + "*");

                    foreach (var key in keys)
                    {
                        await _distributedCache.RemoveAsync(key.ToString());
                    }
                }
            }

            return true;
        }
    }
}
