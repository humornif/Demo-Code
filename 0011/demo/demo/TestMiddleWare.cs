using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace demo
{
    public class TestMiddleWare
    {
        private readonly RequestDelegate _next;
        private static IParaInterface _para;

        public TestMiddleWare(RequestDelegate next, IParaInterface para)
        {
            _next = next;
            _para = para;
        }

        public async Task Invoke(HttpContext context)
        {
            // Do something with context near the beginning of request processing.

            _para.function();

            await _next.Invoke(context);

            // Clean up.
        }
    }
    public static class TestMiddleWareExtensions
    {
        public static IApplicationBuilder UseTestMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TestMiddleWare>();
        }
    }
}
