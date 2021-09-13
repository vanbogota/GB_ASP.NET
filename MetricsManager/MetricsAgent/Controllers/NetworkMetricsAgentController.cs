using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsAgentController : ControllerBase
    {
        //private readonly ILogger<NetworkMetricsAgentController> _logger;

        //public NetworkMetricsAgentController(ILogger<NetworkMetricsAgentController> logger)
        //{
        //    _logger = logger;
        //    _logger.LogDebug(4, "NLog встроен в NetworkMetricsAgentController");
        //}

        [HttpGet("from/{fromTime}/to/{toTime}")]
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
            //_logger.LogInformation($"NetworkCluster - FromTime: {fromTime}, ToTime: {toTime}");
            return Ok();
        }
    }
}
