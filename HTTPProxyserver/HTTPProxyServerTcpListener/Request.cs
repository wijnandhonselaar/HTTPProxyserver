using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace HTTPProxyServerTcpListener
{
    public class Request
    {
        public Request(string context)
        {
            MapToRequest(context);
        }

        public string Method { get; set; }
        public string Url { get; set; }
        public string HttpVersion { get; set; }
        public string UserAgent { get; set; }
        public string Accept { get; set; }
        public string AcceptLanguage { get; set; }
        public string AcceptEncoding { get; set; }
        public string CacheControl { get; set; }
        public string Con { get; set; }
        public string Body { get; set; }
        public string ETag { get; set; }

        public static List<string> RequestToList(string text)
        {
            return text.Split(new [] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }

        private void MapToRequest(string context)
        {
            var head = GetHeaderProperties(context);
            foreach (var property in head)
            {
                switch (property.Key)
                {
                    case "Method":
                        Method = property.Value;
                        break;
                    case "Url":
                        Url = property.Value;
                        break;
                    case "HttpVersion":
                        HttpVersion = property.Value;
                        break;
                    case "User-Agent":
                        UserAgent = property.Value;
                        break;
                    case "Accept":
                        Accept = property.Value;
                        break;
                    case "Accept-Language":
                        AcceptLanguage = property.Value;
                        break;
                    case "Accept-Encoding":
                        AcceptEncoding = property.Value;
                        break;
                    case "Cache-Control":
                        CacheControl = property.Value;
                        break;
                    case "Connection":
                        Con = property.Value;
                        break;
                    case "Body":
                        Body = property.Value;
                        break;
                    case "ETag":
                        ETag = property.Value;
                        break;
                    default:
                        break;
                }
            }
            
        }

        private IEnumerable<HeaderProperty> GetHeaderProperties(string content)
        {
            var lines = RequestToList(content);
            var properties = new List<HeaderProperty>();
            var statusLine = lines[0].Split(' ');
            lines.RemoveAt(0);
            properties.Add(new HeaderProperty("Method", statusLine[0]));
            properties.Add(new HeaderProperty("Url", statusLine[1]));
            properties.Add(new HeaderProperty("HttpVersion", statusLine[2]));
            foreach (var line in lines)
            {
                if(IsProperty(line)) properties.Add(new HeaderProperty(GetKey(line), GetValue(line)));
                
            }
            return properties;
        }

        private bool IsProperty(string line)
        {
            return line.Contains(':');
        }

        private string GetKey(string line)
        {
            return line.Split(':')[0];
        }

        private string GetValue(string line)
        {
            return line.Split(':')[1];
        }

        internal class HeaderProperty
        {
            public HeaderProperty(string key, string value)
            {
                Key = key;
                Value = value;
            }
            public string Key { get; set; }
            public string Value { get; set; }
        }

        //Upgrade-Insecure-Requests: 1
        //Connection: keep-alive
        //Accept-Encoding: gzip, deflate
        //Accept-Language: en-GB,en;q=0.5
        //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
        //User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64; rv:51.0) Gecko/20100101 Firefox/51.0
        //Host: theradavist.com
        //GET http://theradavist.com/ HTTP/1.1
    }
}