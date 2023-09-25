using BookStoreAPI.Helpers;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace BookStoreAPI.middleware
{
    /// <summary>
    /// Here we take in requests and handle potential errors. 
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public ErrorHandlingMiddleware(RequestDelegate requestDelegate) 
        {
            _requestDelegate = requestDelegate;
        }

        /// <summary>
        /// Invokes the error handling if needed. 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        /// <summary>
        /// Writes out JSON errors
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns>Writes JSON Error</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception) 
        {

            var ex = exception as HttpResponseException;

            var exceptionJson = JsonConvert.SerializeObject(new { error = ex.response });

            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(exceptionJson);
        }

    }

    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
