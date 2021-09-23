using System;

namespace MetricsManager.Client
{
    public class DotNetHeapMetrisApiRequest
    {
        public Uri ClientBaseAdress { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}