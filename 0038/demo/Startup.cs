using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace demo
{
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddControllers();
        services.AddRazorPages();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoint =>
        {
            endpoint.MapDefaultControllerRoute();
            endpoint.MapControllers();
            endpoint.MapRazorPages();
            endpoint.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Hello from Demo");
            });
            endpoint.MapGet("/test", async context =>
            {
                await context.Response.WriteAsync("Hello from Demo.Test");
            });

            endpoint.MapGet("/about", async context =>
            {
                await context.Response.WriteAsync("Hello from Demo.About");
            });
        });
    }
}
}
