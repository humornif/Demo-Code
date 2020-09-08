using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace demo
{
    public class ProgramHostedService : IHostedService
    {
        private readonly ILogger _logger;
        public ProgramHostedService(ILogger<ProgramHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting ProgramHostedService registered in Program");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping ProgramHostedService registered in Program");
            return Task.CompletedTask;
        }
    }
}
