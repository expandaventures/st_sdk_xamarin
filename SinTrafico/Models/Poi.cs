using System;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class Poi
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("sequence")]
        public string Sequence { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("category_id")]
        public string CategoryId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
