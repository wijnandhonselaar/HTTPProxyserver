using System;
using System.Net;
using System.Security.Cryptography;

namespace HTTPProxyServerTcpListener
{
    public class CacheItem
    {
        public CacheItem()
        {
        }

        public CacheItem(int maxAge, string type, DateTime date, string head, string body)
        {
            MaxAge = maxAge;
            Type = type;
            Date = date;
            Head = head;
            Body = body;
        }
        
        public int MaxAge { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
    }
}