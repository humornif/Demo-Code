using System;
namespace demo
{
    public class ModelOutput
    {
        public float[] forecasted_count { get; set; }

        public float[] lower_count { get; set; }

        public float[] upper_count { get; set; }
    }
}
