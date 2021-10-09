using AutoMapper;
using MetricsManager.Client;
using MetricsManager.Repositories;
using MetricsManager.Requests;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    /// <summary>
    /// Контроллер для сбора метрик ЦПУ
    /// </summary>
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;
         
        public CpuMetricsController(
            ICpuMetricsRepository repository, 
            ILogger<CpuMetricsController> logger, 
            IMapper mapper, 
            IMetricsAgentClient metricsAgentClient
            )
        {
            _repository = repository;
            _mapper = mapper;
            _metricsAgentClient = metricsAgentClient;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsAgentController");
        }

        /// <summary>
        /// Создание метрики
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _repository.Create(new CpuMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }
        /// <summary>
        /// Получение списка полученных метрик ЦПУ
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            IList<CpuMetric> metrics = _repository.GetAll();

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId, 
            [FromRoute] TimeSpan fromTime, 
            [FromRoute] TimeSpan toTime)
        {
            // логируем, что мы пошли в соседний сервис
            _logger.LogInformation($"starting new request to metrics agent");
            // обращение в сервис
            var metrics = _metricsAgentClient.GetCpuMetrics(new GetAllCpuMetricsApiRequest
            {
                FromTime = fromTime,
                ToTime = toTime
            });

            return Ok(metrics);
            //return Ok();
        }
        /// <summary>
        /// Получение спика метрик за промежуток времени
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster(
            [FromRoute] TimeSpan fromTime, 
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Запрос CPUCluster - FromTime: {fromTime}, ToTime: {toTime}");
            return Ok(_repository.GetByTimePeriod(fromTime, toTime));
        }
    }
}
