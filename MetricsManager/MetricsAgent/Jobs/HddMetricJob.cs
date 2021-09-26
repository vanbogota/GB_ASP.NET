using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _repository;
        private readonly PerformanceCounter _hddCounter;
        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("LogicalDisk", "% Free Space", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var hddMemoryInPercent = Convert.ToInt32(_hddCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _repository.Create(new HddMetric { Time = time, Value = hddMemoryInPercent });
            return Task.CompletedTask;
        }
    }
}
