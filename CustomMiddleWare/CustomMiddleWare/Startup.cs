using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace CustomMiddleWare
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; } // for configuration

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true); // point the configuration builder at a 'local' file

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions(); // Add options to services

            var myOptions = Configuration.GetSection("MyMiddlewareOptionsSec"); // Add the settings section in the json file
            services.Configure<MyMiddlewareOptions>(o => o.OptionOne = myOptions["OptionOne"]); // add to the services this configuration 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello from component one!\n"); // Runs this then moves into next component.
                await next.Invoke(); // Points to the below run statement to run 'next'
                await context.Response.WriteAsync("Hello from component one AGAIN!\n"); // On return from below component we run this.
            });

            app.UseMyMiddleware(); // call my custom middle ware.

            app.Map("/mymapbranch", (appbuilder) => // map this incoming path to THIS pipeline.
            {
                appbuilder.Use(async (context, next) => // Notice this is not "app" anymore but "appbuilder" to hold to this path
                {
                    await context.Response.WriteAsync("Custom middle ware\n");
                    await next.Invoke();
                });

                appbuilder.Run(async (context) =>
                {
                    await context.Response.WriteAsync("Greetings from my map branch!\n");
                });
            });

            app.MapWhen(context => context.Request.Query.ContainsKey("querybranch"), (appbuilder) => // map this for incoming items that have a query string containing 'querybranch'
            {
                appbuilder.Run(async (context) =>
                {
                    await context.Response.WriteAsync("At query branch\n");
                });
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(context.Items["message"].ToString() + "\n"); // referenced passed items from components by using the dictonary as such
                await context.Response.WriteAsync("Hello World!\n");
            });
        }
    }
}
