using MetricsManager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class AgentsController : ControllerBase
    {

        private readonly IAgentsRepository _holder;
        public AgentsController(IAgentsRepository holder)
        {
            _holder = holder;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _holder.Create(agentInfo);
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {            
            return Ok(_holder.GetById(agentId));
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _holder.Delete(agentId);
            return Ok();
        }

        [HttpGet("getlist")]
        public IActionResult RegistredAgents()
        {
            var agentsList = _holder.GetAll();
            return Ok(agentsList);
        }
    }
}
