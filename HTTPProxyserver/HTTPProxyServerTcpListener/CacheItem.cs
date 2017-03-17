using System;
using System.Net;
using System.Security.Cryptography;

namespace HTTPProxyServerTcpListener
{
    public class CacheItem
    {
        /// <summary>
        /// Init CacheItem
        /// </summary>
        /// <param name="maxAge"></param>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <param name="expires"></param>
        /// <param name="head"></param>
        /// <param name="body"></param>
        public CacheItem(int maxAge, string type, DateTime date, DateTime expires, string head, byte[] body)
        {
            MaxAge = maxAge;
            Type = type;
            Date = date;
            Expires = expires;
            Head = head;
            Body = body;
        }
        
        public int MaxAge { get; set; }
        public DateTime Date { get; set; }
        public DateTime Expires { get; set; }
        public string Type { get; set; }
        public string Head { get; set; }
        public byte[] Body { get; set; } // Body is an byte[] to also accept images without having to convert it
    }
}