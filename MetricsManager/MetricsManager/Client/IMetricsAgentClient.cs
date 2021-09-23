using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    interface IMetricsAgentClient
    {
        AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);

        AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);

        DotNetMetricsApiResponse GetDonNetMetrics(DotNetHeapMetrisApiRequest request);

        AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request);
    }
}
