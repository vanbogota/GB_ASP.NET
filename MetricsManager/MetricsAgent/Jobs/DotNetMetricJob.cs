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
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsRepository _repository;
        private readonly PerformanceCounter _dotNetCounter;
        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotNetCounter = new PerformanceCounter(".Net", "gc-heap-size");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var dotNetHeapSize = Convert.ToInt32(_dotNetCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _repository.Create(new DotNetMetric { Time = time, Value = dotNetHeapSize });
            return Task.CompletedTask;
        }
    }
}
