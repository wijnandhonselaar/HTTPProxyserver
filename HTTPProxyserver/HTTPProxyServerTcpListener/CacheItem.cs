using System.Net;

namespace HTTPProxyServerTcpListener
{
    public class CacheItem
    {
        public CacheItem(HttpWebResponse response, string body)
        {
            Response = response;
            Body = body;
        }

        public HttpWebResponse Response { get; set; }
        public string Body { get; set; }
    }
}