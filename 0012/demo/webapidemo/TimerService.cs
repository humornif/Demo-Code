
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace consoledemo
{
    public class TimerService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private int executionCount = 0;

        public TimerService(ILogger<TimerService> logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
            _timer?.Dispose();

        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation($"{DateTime.Now.ToString()} ====== Service proccessing {count}");
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
