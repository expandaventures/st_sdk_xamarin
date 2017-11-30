using System;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class Crs
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }
}
