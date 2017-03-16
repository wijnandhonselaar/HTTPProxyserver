using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace HTTPProxyServerTcpListener
{
    public class HelperService
    {

        public bool AcceptRequest(string contentType)
        {
            var invalid = new List<string> { "video", "audio" };
            return invalid.All(x => !contentType.Contains(x));
        }

        public bool HackRequest(HttpWebResponse webResponse, NetworkStream stream)
        {
            if (AcceptRequest(webResponse.ContentType)) return false;
            var responseStream = webResponse.GetResponseStream();
            responseStream?.CopyTo(stream);
            return true;
        }

        public bool IsImage(string url)
        {
            var options = new[] { "image" };
            foreach (var x in options)
            {
                if (url.Contains(x)) return true;
            }
            return false;
        }

        public void AddHeaders(HttpWebRequest webRequest, Dictionary<string, string> request)
        {
            webRequest.Accept = request["Accept"];
            webRequest.UserAgent = request["User-Agent"];
        }
    }
}