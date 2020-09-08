using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace demo
{
    public class StartupHostedService : IHostedService
    {
        private readonly ILogger _logger;
        public StartupHostedService(ILogger<StartupHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting IHostedService registered in Startup");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping IHostedService registered in Startup");
            return Task.CompletedTask;
        }
    }
}
