using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace webapidemo
{
    public class WorkService : IWorkService
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private int executionCount = 0;

        public WorkService(ILogger<WorkService> logger)
        {
            _logger = logger;
        }

        public async Task DoWork()
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation($"{DateTime.Now.ToString()} ====== Service proccessing {count}");
        }
    }
}
