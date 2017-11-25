using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class Pois
    {
        [JsonProperty("parkings")]
        public List<Parking> Parkings { get; set; }

        [JsonProperty("tolls")]
        public List<Toll> Tolls { get; set; }

        [JsonProperty("gas_stations")]
        public List<GasStation> GasStations { get; set; }
    }
}
