using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare
{
    public class MyMiddlewareClass
    {
        RequestDelegate _next;
        ILoggerFactory _loggerFactory; // dependency injection  from below constructor to send this logger in 
        IOptions<MyMiddlewareOptions> _options;

        public MyMiddlewareClass (RequestDelegate next, ILoggerFactory loggerFactory, IOptions<MyMiddlewareOptions> options)
        {
            _loggerFactory = loggerFactory;
            _next = next;
            _options = options;
        }

        public async Task Invoke (HttpContext context)
        {
            _loggerFactory.AddConsole();
            var logger = _loggerFactory.CreateLogger("my own logger");
            logger.LogInformation("my middle ware class is handling the request.");

            context.Items["message"] = "The weather is nice"; // to pass data between the middleware us this 'IDictonary<object, object>'

            await context.Response.WriteAsync(_options.Value.OptionOne + "\n"); // reference options defined in appsettings but placed into IOptions
            await context.Response.WriteAsync("My middle ware class stuff\n");
            await _next.Invoke(context);
            await context.Response.WriteAsync("End of my middle ware class stuff\n");
        }
    }

    public class MyMiddlewareOptions
    {
        public string OptionOne { get; set; }
    }

    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddlewareClass>();
        }
    }
}
