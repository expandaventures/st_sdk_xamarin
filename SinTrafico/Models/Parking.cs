﻿using System;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class Parking
    {
        [JsonProperty("geometry")]
        public PoiGeometry Geometry { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("eta")]
        public double Eta { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }
    }
}
