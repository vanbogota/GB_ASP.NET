using MetricsAgent.Controllers;
using MetricsAgent.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly PerformanceCounter _cpuCounter;
        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercent = Convert.ToInt32(_cpuCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _repository.Create(new CpuMetric { Time = time, Value = cpuUsageInPercent });
            return Task.CompletedTask;
        }
    }
}
