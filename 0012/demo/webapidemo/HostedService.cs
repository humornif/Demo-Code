using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace webapidemo
{
    public class HostedService : IHostedService, IDisposable
    {
        private readonly ILogger<HostedService> _logger;
        public IServiceProvider Services { get; }
        private Timer _timer;

        public HostedService(IServiceProvider services, ILogger<HostedService> logger)
        {
            Services = services;
            _logger = logger;
        }

            public void Dispose()
        {
            _timer?.Dispose();
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("====== Service working");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IWorkService>();

                scopedProcessingService.DoWork().GetAwaiter().GetResult();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("====== Service starting");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("====== Service stopping");

            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
