using System;
using Microsoft.ML.Data;

namespace demo
{
    public class ModelInput
    {
        [LoadColumn(0)]
        public DateTime action_time { get; set; }

        [LoadColumn(1)]
        public float count { get; set; }
    }
}
