using MetricsManager.Interfaces;
using MetricsManager.Models;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IRamNetMetricsRepository _repository;
        private readonly PerformanceCounter _ramCounter;
        public RamMetricJob(IRamNetMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var ramUsageInMByte = Convert.ToInt32(_ramCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _repository.Create(new RamMetric { Time = time, Value = ramUsageInMByte });
            return Task.CompletedTask;
        }
    }
}
