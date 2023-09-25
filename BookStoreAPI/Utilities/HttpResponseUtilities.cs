using BookStoreAPI.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BookStoreAPI.Helpers
{
    public class HttpResponseUtilities
    {
        /// <summary>
        /// Throws an exception.
        /// </summary>
        /// <param name="msg">Custom message</param>
        /// <param name="statusCode">Set a fitting error code.</param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public static HttpResponseMessage ThrowHttpException(string msg, HttpStatusCode statusCode)
        {
            const string contentType = "Application/json; charset=utf-8";

            HttpResponseMessage response = new HttpResponseMessage();
            response.Headers.Add("ContentType", contentType);
            response.Content.Headers.Add("ContentType", contentType);
            response.StatusCode = statusCode;
            response.ReasonPhrase = msg;

            throw new HttpResponseException(response);
        }
    }

}
