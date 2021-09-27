using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public class BaseMetricsApiResponse<Metric> where Metric : class
    {
       public IList<Metric> Metrics { get; set; }
    }
}
