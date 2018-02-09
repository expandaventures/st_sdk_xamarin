using System;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class GasStation
    {
        [JsonProperty("geometry")]
        public PoiGeometry Geometry { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("eta")]
        public double Eta { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }
    }
}
