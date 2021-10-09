using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerClient.Services
{
    public class CpuMetricResponce
    {
        public IList<CpuMetric> Metrics { get; set; }
    }
}
