using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace RestAPITestDemo
{
    public enum Verb
    {
        GET, 
        POST, 
        PUT, 
        DELETE
    }

    class Program
    {
        static void Main(string[] args)
        {
            // start...
            var client = new Client();
            client.EndPoint = @"http://jsonplaceholder.typicode.com";
            // client.Method = Verb.GET;
            var pdata = client.PostData;
            var response = client.Request("/posts/1");
            Console.WriteLine(response);
        }

        
    }
    public class Client
    {
        public string EndPoint { get; set; }
        public Verb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }

        public Client()
        {
            EndPoint = "";
            Method = Verb.GET;
            ContentType = "application/JSON";
            PostData = "";      
        }

        public Client(string endpoint, Verb method, string posatData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/json";
            PostData = PostData;
        }

        public string Request()
        {
            return Request("");        
        }

        public string Request(string parameter)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameter);
            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Failed: Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);        
                }

                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }
                return responseValue;
            }
        }

    }
}
