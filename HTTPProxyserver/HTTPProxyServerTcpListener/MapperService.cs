using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTPProxyServerTcpListener
{
    public class MapperService
    {
        public string GetHead(HttpWebResponse httpRes)
        {
            var head = "";
            // status line
            head += "HTTP/" + httpRes.ProtocolVersion + " " + httpRes.StatusCode.GetHashCode() + " " +
                    httpRes.StatusDescription + "\r\n";
            // Date
            head += ToHeaderProperty("Date", httpRes.Headers["Date"]);
            // Server
            head += ToHeaderProperty("Server", httpRes.Server);
            // Content-Type
            head += ToHeaderProperty("Content-Type", httpRes.ContentType);
            // Content-Length
            head += ToHeaderProperty("Content-Length", httpRes.ContentLength.ToString());
            
            return head;
        }

        private string ToHeaderProperty(string name, string value)
        {
            return name + ": " + value + "\r\n";
        }

        public Dictionary<string, string> ToHead(string context)
        {
            var request = new Dictionary<string, string>();
            var lines = Request.RequestToList(context);
            for (var i = 0; i < lines.Count; i++)
            {
                if (i > 0)
                {
                    if (lines[i].Contains(':'))
                    {
                        request.Add(lines[i].Split(':')[0], lines[i].Split(':')[1].Trim());
                    }
                    else if (lines[i] == "\r\n")
                    {
                        var body = "";
                        for (var y = i + 1; y < lines.Count; y++)
                        {
                            body += lines[y];
                        }
                        request.Add("Body", body);
                        i = lines.Count;
                    }
                }
                else
                {
                    var statusLine = lines[i].Split(' ');
                    request.Add("Method", statusLine[0]);
                    request.Add("Url", statusLine[1]);
                    request.Add("HttpVersion", statusLine[2]);
                }
            }
            return request;
        }

        public byte[] MapToResponse(string head, byte[] body)
        {
            var response = new List<byte>(Encoding.UTF8.GetBytes(head));
            response.AddRange(Encoding.UTF8.GetBytes("\r\n"));
            response.AddRange(body);
            return response.ToArray();
        }
    }
}