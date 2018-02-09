using System;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class UserPoi
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("geometry")]
        public PoiGeometry Geometry { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("eta")]
        public double Eta { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }
    }
}
