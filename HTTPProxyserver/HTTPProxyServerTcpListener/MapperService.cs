using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTPProxyServerTcpListener
{
    /// <summary>
    /// Mapper service contains functions that have to do with changing data to another data-type
    /// </summary>
    public class MapperService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpRes"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns key and value as one string in the format used in a http header
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ToHeaderProperty(string key, string value)
        {
            return key + ": " + value + "\r\n";
        }

        /// <summary>
        /// converts the response or request from string to dictionary, to make the data more accessable.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>request/response head</returns>
        public Dictionary<string, string> ToHead(string context)
        {
            var request = new Dictionary<string, string>();
            var lines = RequestToList(context);
            for (var i = 0; i < lines.Count; i++)
            {
                // First line is the status line
                // erveything until the empty line is a header
                // the forloop after the empty line is in case the content is split in more than one line because of the split on Environment.NewLine
                if (i > 0)
                {
                    if (lines[i].Contains(':'))
                    {
                        request.Add(lines[i].Split(':')[0], lines[i].Split(':')[1].Trim());
                    }
                    else if (lines[i] == "")
                    {
                        var body = "";
                        for (var y = i + 1; y < lines.Count; y++)
                        {
                            body += lines[y];
                        }
                        request.Add("Body", body.Trim());
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

        /// <summary>
        /// Returns the request/response as a list of lines.
        /// Splits on every NewLine in the string ("\r\n" for example)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<string> RequestToList(string text)
        {
            return text.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }

        /// <summary>
        /// returns byte[] of the complete webResponse
        /// Combines head and body into one object.
        /// </summary>
        /// <param name="head"></param>
        /// <param name="body"></param>
        /// <returns>response</returns>
        public byte[] MapToResponse(string head, byte[] body)
        {
            // Creating a list because this is easier to manupulate in comparrison to the array it is.
            var response = new List<byte>(Encoding.UTF8.GetBytes(head));
            response.AddRange(Encoding.UTF8.GetBytes("\r\n"));
            response.AddRange(body);
            return response.ToArray();
        }
    }
}