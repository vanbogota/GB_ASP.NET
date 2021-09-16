using AutoMapper;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsAgentController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsAgentController> _logger;        

        private IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;
        public DotNetMetricsAgentController(IDotNetMetricsRepository repository, ILogger<DotNetMetricsAgentController> logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(2, "NLog встроен в DotNetMetricsAgentController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _repository.Create(new DotNetMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            IList<DotNetMetric> metrics = _repository.GetAll();

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            //_logger.LogInformation($"Agent - FromTime: {fromTime}, ToTime: {toTime}");
            return Ok();
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
            //_logger.LogInformation($"DotNetCluster - FromTime: {fromTime}, ToTime: {toTime}");
            return Ok();
        }
    }
}
