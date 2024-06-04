using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Helpers.MemoryCache
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CacheManager> _logger;

        public CacheManager(IMemoryCache cache, ILogger<CacheManager> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Set(string key, object value, int time)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cache key cannot be null or empty", nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            try
            {
                _cache.Set(key, value, TimeSpan.FromMinutes(time));
                _logger.LogInformation($"Cache set: {key}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error setting cache for key: {key}");
                throw;
            }

            return Task.CompletedTask;
        }

        public Task<T> Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cache key cannot be null or empty", nameof(key));
            }

            try
            {
                var value = _cache.Get<T>(key);
                if (value != null)
                {
                    _logger.LogInformation($"Cache hit: {key}");
                }
                else
                {
                    _logger.LogInformation($"Cache miss: {key}");
                }

                return Task.FromResult(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting cache for key: {key}");
                throw;
            }
        }

        public Task Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cache key cannot be null or empty", nameof(key));
            }

            try
            {
                _cache.Remove(key);
                _logger.LogInformation($"Cache removed: {key}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing cache for key: {key}");
                throw;
            }

            return Task.CompletedTask;
        }
    }
}
