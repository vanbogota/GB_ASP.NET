﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Requests
{
    public class BaseRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}
