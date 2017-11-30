using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class Route
    {
        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("legs")]
        public List<Leg> Legs { get; set; }

        [JsonProperty("geometry")]
        public object Geometry { get; set; }

        [JsonProperty("pois")]
        public Pois Pois { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("cost")]
        public double? Cost { get; set; }

        public string AsGeoJson() => Geometry.ToString();

        public Geometry AsGeometry() => Geometry as Geometry;
    }
}
