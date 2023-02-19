﻿using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace TenApplication.Helpers
{
    public static class CacheHelper
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
                                                   string recordId,
                                                   T data,
                                                   TimeSpan? absoluteExpireTime = null,
                                                   TimeSpan? slidingExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = slidingExpireTime;
            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        public static async Task<T?> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);

            if (await cache.GetStringAsync(recordId) is null) return default(T);

            return JsonSerializer.Deserialize<T>(jsonData!);
        }

        public static async Task<T?> DeleteRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            if (await cache.GetStringAsync(recordId) is not null) await cache.RemoveAsync(recordId);

            return default(T);
        }
    }
}
