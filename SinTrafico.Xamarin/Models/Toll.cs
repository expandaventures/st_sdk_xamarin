using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class Toll
    {
        [JsonProperty("geometry")]
        public PoiGeometry Geometry { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rates")]
        public List<object> Rates { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
