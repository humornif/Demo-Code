using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace demo
{
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureServices(services => services.AddHostedService<ProgramHostedService>());

    //public static IHostBuilder ConfigureWebHost(this IHostBuilder builder, Action<IWebHostBuilder> configure)
    //{
    //    var webhostBuilder = new GenericWebHostBuilder(builder);

    //    // This calls the lambda function in Program.cs, and registers your services using Startup.cs
    //    configure(webhostBuilder);

    //    // Adds the GenericWebHostService
    //    builder.ConfigureServices((context, services) => services.AddHostedService<GenericWebHostService>());
    //    return builder;
    //}
}
}
