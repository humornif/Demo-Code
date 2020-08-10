using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace demo
{
    public class ValidateReferrerAttribute : ActionFilterAttribute
    {
        private IConfiguration _configuration;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _configuration = (IConfiguration)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));

            base.OnActionExecuting(context);

            if (!IsValidRequest(context.HttpContext.Request))
            {
                context.Result = new ContentResult
                {
                    Content = $"Invalid referer header",
                };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
            }
        }
        private bool IsValidRequest(HttpRequest request)
        {
            string referrerURL = "";

            if (request.Headers.ContainsKey("Referer"))
            {
                referrerURL = request.Headers["Referer"];
            }
            if (string.IsNullOrWhiteSpace(referrerURL)) return true;
              
            var allowedUrls = _configuration.GetSection("CorsOrigin").Get<string[]>()?.Select(url => new Uri(url).Authority).ToList();

            //for Swagger & Postman, uncomment codes under.
            //var host = request.Host.Value;
            //allowedUrls.Add(host);

            bool isValidClient = allowedUrls.Contains(new Uri(referrerURL).Authority);
            
            return isValidClient;
        }
    }
}
