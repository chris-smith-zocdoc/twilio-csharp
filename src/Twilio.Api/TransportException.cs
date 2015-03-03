using System;
using System.Linq;
using System.Text;
using RestSharp;

namespace Twilio
{
    public class TransportException : Exception
    {
        public IRestResponse Response { get; private set; }

        public TransportException(IRestResponse response)
            : base(response.ErrorMessage, response.ErrorException)
        {
            Response = response;
        }

        public override string ToString()
        {
            var headerStrings = string.Join(", ", Response.Headers.Select(h => h.ToString()).ToArray());

            var content = "";
            if (Response.RawBytes != null && Response.RawBytes.Length > 0)
            {
                content = Encoding.UTF8.GetString(Response.RawBytes);
            }

            return string.Format("error message: {0}\n" +
                                 "error exception: {1}\n" +
                                 "status code: {2}\n" +
                                 "status desc: {3}\n" +
                                 "response status: {4}\n" +
                                 "server: {5}\n" +
                                 "response uri: {6}\n" +
                                 "headers: {7}\n" +
                                 "content: {8}\n",

                                 Response.ErrorMessage,
                                 Response.ErrorException,
                                 Response.StatusCode,
                                 Response.StatusDescription,
                                 Response.ResponseStatus,
                                 Response.Server,
                                 Response.ResponseUri,
                                 headerStrings,
                                 content
                                 );
        }
    }
}
