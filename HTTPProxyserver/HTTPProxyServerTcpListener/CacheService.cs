using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTPProxyServerTcpListener
{
    public class CacheService
    {
        public ConcurrentDictionary<string, CacheItem> _cache = new ConcurrentDictionary<string, CacheItem>();

        public void AddToCache(HttpWebResponse httpRes, string head, byte[] content, int maxAge)
        {
            var type = httpRes.ContentType;
            var date = DateTime.Now;
            var item = new CacheItem(maxAge, type, date, head, content);
            _cache.AddOrUpdate(httpRes.ResponseUri.AbsoluteUri, item, (key, oldValue) => item);
        }

        public bool IsCached(Dictionary<string, string> request, out byte[] response)
        {
            response = new byte[1];
            if (!_cache.ContainsKey(request["Url"])) return false;
            CacheItem cached;
            _cache.TryGetValue(request["Url"], out cached);
            if ((DateTime.Now - cached.Date).TotalSeconds > cached.MaxAge) return false;
            if (cached.Head.Contains("image"))
            {
                response = cached.Body;
                return true;
            }
            var x = Encoding.UTF8.GetBytes(cached.Head).ToList();
            x.AddRange(Encoding.UTF8.GetBytes("\r\n"));
            x.AddRange(cached.Body);
            response = x.ToArray();
            return true;
        }

        public void ClearCache()
        {
            _cache.Clear();
        }
    }
}