using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);

        AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);

        DotNetMetricsApiResponse GetDotNetMetrics(DotNetHeapMetrisApiRequest request);

        AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request);
    }
}
