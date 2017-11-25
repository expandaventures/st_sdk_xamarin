using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class PoiGeometry
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }
    }
}
