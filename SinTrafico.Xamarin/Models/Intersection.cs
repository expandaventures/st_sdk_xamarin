using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class Intersection
    {
        [JsonProperty("entry")]
        public List<bool> Entry { get; set; }

        [JsonProperty("bearings")]
        public List<int> Bearings { get; set; }

        [JsonProperty("location")]
        public List<double> Location { get; set; }

        [JsonProperty("out")]
        public int? Out { get; set; }

        [JsonProperty("in")]
        public int? In { get; set; }
    }
}
