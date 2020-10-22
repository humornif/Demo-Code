using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace demo
{
    [DisallowConcurrentExecution]
    public class DemoJob : IJob
    {
        private readonly ILogger<DemoJob> _logger;
        public DemoJob(ILogger<DemoJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Demo !");
            return Task.CompletedTask;
        }
    }
}
