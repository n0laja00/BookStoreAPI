using BookStoreAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace BookStoreAPI.Helpers
{
    public class HttpResponseUtilities
    {
        /// <summary>
        /// Makes an Http Response message with msg and status code
        /// </summary>
        /// <param name="msg">Custom message</param>
        /// <param name="statusCode">Set a fitting error code.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseMessage">Http response message</exception>
        public static HttpResponseMessage HttpResponseMessageMaker(string msg, HttpStatusCode statusCode)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = statusCode;
            response.ReasonPhrase = msg;

            return response;
        }
    }

}
