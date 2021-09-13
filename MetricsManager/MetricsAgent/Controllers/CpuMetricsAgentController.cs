using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using System;
using System.Data.SQLite;
using MetricsAgent.Repositories;
using System.Collections.Generic;
using MetricsAgent.Requests;
using MetricsAgent.Responses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsAgentController : ControllerBase
    {
        //private readonly ILogger<CpuMetricsAgentController> _logger;

        //public CpuMetricsAgentController(ILogger<CpuMetricsAgentController> logger)
        //{
        //    _logger = logger;
        //    _logger.LogDebug(1, "NLog встроен в CpuMetricsAgentController");
        //}        
        private ICpuMetricsRepository repository;
        public CpuMetricsAgentController(ICpuMetricsRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            repository.Create(new CpuMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = repository.GetAll();

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }  

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            //_logger.LogInformation($"AgentId: {agentId}, FromTime: {fromTime}, ToTime: {toTime}");
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            //_logger.LogInformation($"CPUCluster - FromTime: {fromTime}, ToTime: {toTime}");
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            //_logger.LogInformation($"Agent - FromTime: {fromTime}, ToTime: {toTime}");
            return Ok();
        }        
    }
}
