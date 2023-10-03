using AngleSharp.Io;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models;
using Ganss.Xss;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;

namespace BookStoreAPI.middleware
{
    /// <summary>
    /// This Class is designed to fight XSS attacks.
    /// </summary>
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        //private AntiXssErrorResponse _error;
        private readonly int _statusCode = (int)HttpStatusCode.BadRequest;

        public AntiXssMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
 
        }

        /// <summary>
        /// Invoker function for this class. It calls ReadRequestBody and, if needed, RespondWithAnError.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            //Stored to be re-used
            var storedBody = context.Request.Body;

            try
            {
                var content = await ReadRequestBody(context);
                var url = context.Request.QueryString.ToString();

                //sanitise body
                var sanitiserBody = new HtmlSanitizer();
                var sanitisedBody = sanitiserBody.Sanitize(content);

                //sanitize url
                var sanitiserUrl = new HtmlSanitizer();
                var sanitisedUrl = sanitiserUrl.Sanitize(url);

                //compare results
                if (content.Replace("\r", "") != sanitisedBody.Replace("&amp;", "&"))
                {
                    await RespondWithAnError(context);
                }
                else if (url != sanitisedUrl.Replace("&amp;", "&"))
                {
                    await RespondWithAnError(context);
                }
                await _requestDelegate(context);
            }
            finally
            {
                context.Request.Body = storedBody;
            }
        }

        /// <summary>
        /// This function reads the request's body and returns its contents to caller.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Request's body</returns>
        private static async Task<string> ReadRequestBody(HttpContext context)
        {
            var buffer = new MemoryStream();
            await context.Request.Body.CopyToAsync(buffer);
            var testi = HttpUtility.JavaScriptStringEncode(context.Request.QueryString.ToString());
            context.Request.Body = buffer;
            buffer.Position = 0;

            var encoding = Encoding.UTF8;

            var requestContent = await new StreamReader(buffer, encoding).ReadToEndAsync();
            context.Request.Body.Position = 0;

            return requestContent;
        }



        /// <summary>
        /// Responds with an error.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Internal Server Error 500</returns>
        private async Task RespondWithAnError(HttpContext context)
        {
            context.Response.Clear();
            context.Response.Headers.Add("ContentType", "Application/json;");
            context.Response.StatusCode = _statusCode;

            HttpResponseUtilities.ThrowHttpException("Xss warning.", HttpStatusCode.InternalServerError);
        }


    }

    public static class AntiXssExtension
    {
        public static IApplicationBuilder UseAntiXssMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AntiXssMiddleware>();
        }
    }

}
