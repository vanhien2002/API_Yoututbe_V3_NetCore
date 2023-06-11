using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Drawing.Printing;

namespace BlueTube.NETCore.Models
{
    public static class CacheManager
    {
        private static readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());        
        public static void addToCache(String key, object value)
        {
            // lưu trữ đối tượng vào cache với tùy chọn thời gian tự động xóa sau 5 phút
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(key, value, cacheEntryOptions);
        }
        public static object getFromCache(string key)
        {
            // lấy đối tượng từ cache
            var myObject = _cache.Get<object>(key);
            return myObject;
        }
        public static void removeCache(string key)
        {
            _cache.Remove(key);
        }
    }
}
