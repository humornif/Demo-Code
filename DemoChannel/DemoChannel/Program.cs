using System.Threading.Tasks;
using DemoChannel.Channels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DemoChannel
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost Host = CreateHostBuilder(args).Build();

            Consumer.OnData += DataHandle.Handler;
            Task.Run(() => Consumer.BeginReceive());

            await Host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
