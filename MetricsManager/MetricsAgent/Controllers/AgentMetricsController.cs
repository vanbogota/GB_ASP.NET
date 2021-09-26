using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    /// <summary>
    /// Контроллер для сбора агентов
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AgentMetricsController : ControllerBase
    {
        private readonly ValuesHolder _holder;
        public AgentMetricsController(ValuesHolder holder)
        {
            _holder = holder;
        }
        /// <summary>
        /// Регистрируем агента
        /// </summary>
        /// <param name="agentInfo"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Получаем список всех зарегистрированных агентов
        /// </summary>
        /// <returns></returns>
        [HttpGet("getlist")]
        public IActionResult RegistredAgents()
        {
            var agentsList = _holder.Agents;
            return Ok(agentsList);
        }
    }
}
