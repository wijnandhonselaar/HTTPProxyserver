using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace HTTPProxyServerTcpListener
{
    public class HelperService
    {
        /// <summary>
        /// return true in contentType of the request is not video or audio.
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public bool AcceptRequest(string contentType)
        {
            var invalid = new List<string> { "video", "audio" };
            return invalid.All(x => !contentType.Contains(x));
        }

        /// <summary>
        /// If the request is video or audio
        /// pass the entire stream onto the client (No caching!!) So no points there.
        /// </summary>
        /// <param name="webResponse"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public bool HackRequest(HttpWebResponse webResponse, NetworkStream stream)
        {
            if (AcceptRequest(webResponse.ContentType)) return false;
            var responseStream = webResponse.GetResponseStream();
            responseStream?.CopyTo(stream);
            return true;
        }

        /// <summary>
        /// Determines whether a request expects an image.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool IsImage(string url)
        {
            var options = new[] { "jpg", "tif", "png", "gif" };
            foreach (var x in options)
            {
                if (url.Contains(x)) return true;
            }
            return false;
        }

        /// <summary>
        /// Changes head properties to null to hide informtion about the client
        /// </summary>
        /// <param name="webRequest"></param>
        /// <param name="request"></param>
        public void HideMe(HttpWebRequest webRequest, Dictionary<string, string> request)
        {
            webRequest.UserAgent = null;
            webRequest.Host = null;
        }
    }
}