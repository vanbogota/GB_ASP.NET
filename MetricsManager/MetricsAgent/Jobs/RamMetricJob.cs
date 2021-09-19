using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Repositories;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private IRamNetMetricsRepository _repository;
        private PerformanceCounter _ramCounter;
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
