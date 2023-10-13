using System.Runtime.Serialization;

namespace BookStoreAPI.Models
{
    [Serializable]
    public class HttpResponseException : Exception
    {
        /// <summary>
        /// Custom HttpResponseException Class for error handling 
        /// </summary>

        public HttpResponseMessage response;

        public HttpResponseException(HttpResponseMessage response)
        {
            this.response = response;
        }

        public HttpResponseMessage? Response { get; set; }
    }
}