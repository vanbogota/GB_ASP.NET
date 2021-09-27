using MetricsManager.Client;
using MetricsManager.Interfaces;
using MetricsManager.Models;
using Quartz;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IAgentsRepository _agentsRepository;
        private readonly IRamMetricsRepository _repository;
        private readonly IMetricsAgentClient _metricsAgentClient;
        public RamMetricJob(
            IRamMetricsRepository repository,
            IMetricsAgentClient metricsAgentClient,
            IAgentsRepository agentsRepository)
        {
            _repository = repository;
            _metricsAgentClient = metricsAgentClient;
            _agentsRepository = agentsRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var agentsList = _agentsRepository.GetAll();
            foreach (var agent in agentsList)
            {
                var allMetricsApiResponce = _metricsAgentClient.GetAllRamMetrics(new GetAllRamMetricsApiRequest
                {
                    ClientBaseAdress = agent.AgentAdress,
                    FromTime = _repository.GetAll().Max(p => p.Time),
                    ToTime = DateTime.UtcNow.TimeOfDay,
                });

                foreach (var ramMetric in allMetricsApiResponce.Metrics)
                {
                    _repository.Create(ramMetric);
                }
            }
            return Task.CompletedTask;
        }
    }
}
