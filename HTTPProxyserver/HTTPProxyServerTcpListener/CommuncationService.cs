using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTPProxyServerTcpListener
{
    public class CommuncationService
    {
        public void SendResponse(byte[] response, Stream stream)
        {
            stream.Write(response, 0, response.Length);
        }

        public byte[] Response(HttpWebResponse httpRes, string head)
        {
            var responseBody = new byte[] { };
            var resStream = httpRes.GetResponseStream();
            if (resStream == null) return null;
            if (new[] { "image", "video" }.Any(c => httpRes.ContentType.Contains(c)))
            {
                using (var reader = new BinaryReader(resStream))
                    responseBody = reader.ReadBytes((int)httpRes.ContentLength);
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