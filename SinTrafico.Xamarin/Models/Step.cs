using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class Step
    {
        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("intersections")]
        public List<Intersection> Intersections { get; set; }
    }
}
