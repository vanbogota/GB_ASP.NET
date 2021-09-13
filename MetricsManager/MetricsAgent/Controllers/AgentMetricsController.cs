using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentMetricsController : ControllerBase
    {
        private readonly ValuesHolder _holder;
        public AgentMetricsController(ValuesHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _holder.Agents.Add(agentInfo);
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpGet("getlist")]
        public IActionResult RegistredAgents()
        {
            return Ok(_holder);
        }
    }
}
