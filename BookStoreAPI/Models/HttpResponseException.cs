using System.Runtime.Serialization;

namespace BookStoreAPI.Models
{
    [Serializable]
    internal class HttpResponseException : Exception
    {
        /// <summary>
        /// Custom HttpResponseException Class for error handling 
        /// </summary>
        public HttpResponseMessage response;

        public HttpResponseException()
        {
        }

        public HttpResponseException(HttpResponseMessage response)
        {
            this.response = response;
        }

        public HttpResponseException(string? message) : base(message)
        {
        }

        public HttpResponseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected HttpResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public object Response { get; internal set; }
    }
}