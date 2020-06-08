using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOptions();

            services.AddTransient<IParaInterface, ParaClass>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello, World! 1111");
                await next.Invoke();
            });

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello, World!");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Map("/map1", HandleMapTest1);
            app.Map("/test1", app =>
            {
                app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Hello, World! 1111");
                    await next.Invoke();
                });
            });
            app.Map("/router", router =>
            {
                app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Hello, MAP1! 1111");
                    await next.Invoke();
                });
            });
            //app.UseMiddleware<TestMiddleWare>();
            app.UseTestMiddleWare();
        }


        private void HandleMapTest1(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello, MAP1! 1111");
                await next.Invoke();
            });
        }
    }
}
