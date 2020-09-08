using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace demo
{
public class Service2 : IHostedService
{
    private readonly ILogger _logger;
    public Service2(ILogger<Service2> logger)
    {
        _logger = logger;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting Service2");
        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stoping Service2");
        return Task.CompletedTask;
    }
}
}
