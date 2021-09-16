using MetricsAgent.Models;
using MetricsAgent.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Interfaces
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {
    }
}
