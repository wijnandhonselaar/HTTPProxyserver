using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTPProxyServerTcpListener
{
    public class CommuncationService
    {
        /// <summary>
        /// Send the response over the given stream
        /// </summary>
        /// <param name="response"></param>
        /// <param name="stream"></param>
        public void SendResponse(byte[] response, Stream stream)
        {
            stream.Write(response, 0, response.Length);
        }

        /// <summary>
        /// GetResponseBody reads the stream of the WebResponse
        /// returns the body of the response
        /// </summary>
        /// <param name="webResponse"></param>
        /// <param name="head"></param>
        /// <returns></returns>
        public byte[] GetResponseBody(HttpWebResponse webResponse, string head)
        {
            byte[] responseBody;
            var resStream = webResponse.GetResponseStream();
            if (resStream == null) return null;
            // if ContentType is image, use BinaryReader
            // 
            if (webResponse.ContentType.Contains("image"))
            {
                using (var reader = new BinaryReader(resStream))
                    responseBody = reader.ReadBytes((int)webResponse.ContentLength);
            }
            else
            {
                using (var reader = new StreamReader(resStream))
                    responseBody = Encoding.UTF8.GetBytes(reader.ReadToEnd());
            }
            return responseBody;
        }
    }
}