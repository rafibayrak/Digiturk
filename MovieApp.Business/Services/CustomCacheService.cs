using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MovieApp.Business.Services.IServices;
using MovieApp.Data.Core;
using Newtonsoft.Json;

namespace MovieApp.Business.Services
{
	public class CustomCacheService: ICustomCacheService
    {
        private IOptions<AppSettings> _appSettings;
        private readonly IMemoryCache _memoryCache;
        private readonly IRedisCacheService _redisCacheService;

        public CustomCacheService(IOptions<AppSettings> appSettings, IMemoryCache memoryCache, IRedisCacheService redisCacheService)
        {
            _appSettings = appSettings;
            _memoryCache = memoryCache;
            _redisCacheService = redisCacheService;
        }

        public T Get<T>(string key)
        {
            switch (_appSettings.Value.CacheType)
            {
                case "redis":
                    var value = _redisCacheService.GetValue(key);
                    if (string.IsNullOrEmpty(value))
                    {
                        return default(T);
                    }

                    return JsonConvert.DeserializeObject<T>(value);
                case "runtime":
                    return (T)_memoryCache.Get(key);
                case "rediswithruntime":
                    var memory = _memoryCache.Get(key);
                    if (memory == null)
                    {
                        var redis = _redisCacheService.GetValue(key);
                        if (string.IsNullOrEmpty(redis))
                        {
                            return default(T);
                        }

                        return JsonConvert.DeserializeObject<T>(redis);
                    }
                    else
                    {
                        return (T)memory;
                    }
                default:
                    return default(T);
            }
        }

        public void Create(string key, object data, TimeSpan timeSpan) {
            switch (_appSettings.Value.CacheType)
            {
                case "redis":
                    _redisCacheService.SetValue(key,JsonConvert.SerializeObject(data), timeSpan);
                    break;
                case "runtime":
                    _memoryCache.GetOrCreate(key, (ICacheEntry arg) => {
                        arg.SetAbsoluteExpiration(timeSpan);
                        return data;
                    });
                    break;
                case "rediswithruntime":
                    _memoryCache.GetOrCreate(key, (ICacheEntry arg) => {
                        arg.SetAbsoluteExpiration(timeSpan);
                        return data;
                    });
                    _redisCacheService.SetValue(key, JsonConvert.SerializeObject(data), timeSpan);
                    break;
            }
        }

        public async Task<T> GetAsync<T>(string key)
        {
            switch (_appSettings.Value.CacheType)
            {
                case "redis":
                    var value = await _redisCacheService.GetValueAsync(key);
                    if (string.IsNullOrEmpty(value))
                    {
                        return default(T);
                    }

                    return JsonConvert.DeserializeObject<T>(value);
                case "runtime":
                    return (T)_memoryCache.Get(key);
                case "rediswithruntime":
                    var memory = _memoryCache.Get(key);
                    if (memory == null)
                    {
                        var redis = await _redisCacheService.GetValueAsync(key);
                        if (string.IsNullOrEmpty(redis))
                        {
                            return default(T);
                        }

                        return JsonConvert.DeserializeObject<T>(redis);
                    }
                    else
                    {
                        return (T)memory;
                    }
                default:
                    return default(T);
            }
        }

        public async Task CreateAsync(string key, object data, TimeSpan timeSpan)
        {
            switch (_appSettings.Value.CacheType)
            {
                case "redis":
                    await _redisCacheService.SetValueAsync(key, JsonConvert.SerializeObject(data), timeSpan);
                    break;
                case "runtime":
                    await _memoryCache.GetOrCreateAsync(key, async (ICacheEntry arg) => {
                        arg.SetAbsoluteExpiration(timeSpan);
                        return data;
                    });
                    break;
                case "rediswithruntime":
                    await _memoryCache.GetOrCreateAsync(key, async (ICacheEntry arg) => {
                        arg.SetAbsoluteExpiration(timeSpan);
                        return data;
                    });
                    await _redisCacheService.SetValueAsync(key, JsonConvert.SerializeObject(data), timeSpan);
                    break;
            }
        }


        public void Remove(string key) {
            switch (_appSettings.Value.CacheType)
            {
                case "redis":
                    _redisCacheService.Clear(key);
                    break;
                case "runtime":
                    _memoryCache.Remove(key);
                    break;
                case "rediswithruntime":
                    _redisCacheService.Clear(key);
                    _memoryCache.Remove(key);
                    break;
            }
        }

        public void Clear() {
            switch (_appSettings.Value.CacheType)
            {
                case "redis":
                    _redisCacheService.ClearAll();
                    break;
                case "runtime":
                    if (_memoryCache is MemoryCache memoryCache)
                    {
                        memoryCache.Clear();

                    }

                    break;
                case "rediswithruntime":
                    if (_memoryCache is MemoryCache mCache)
                    {
                        mCache.Clear();
                        _redisCacheService.ClearAll();
                    }
                    break;
            }
        }
    }
}

