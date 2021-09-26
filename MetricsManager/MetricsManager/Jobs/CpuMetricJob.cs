using MetricsManager.Client;
using MetricsManager.Controllers;
using MetricsManager.Interfaces;
using MetricsManager.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly IAgentsRepository _agentsRepository;
        private readonly ICpuMetricsRepository _repository;
        private readonly IMetricsAgentClient _metricsAgentClient;
        public CpuMetricJob(
            ICpuMetricsRepository repository, 
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
               var temp = _metricsAgentClient.GetCpuMetrics(new GetAllCpuMetricsApiRequest 
                {
                   ClientBaseAdress = agent.AgentAdress,
                   FromTime = _repository.GetAll().Max(p => p.Time),
                   ToTime = DateTime.UtcNow.TimeOfDay,
                });            

            }
            return Task.CompletedTask;
        }
    }
}
