using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class PoiResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("pois")]
        public List<Poi> Pois { get; set; }
    }
}
