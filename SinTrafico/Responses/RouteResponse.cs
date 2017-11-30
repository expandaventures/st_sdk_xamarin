using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class RouteResponse
    {
        [JsonProperty("apiKey")]
        public string ApiKey { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("req_id")]
        public string ReqId { get; set; }

        [JsonProperty("routes")]
        public List<Route> Routes { get; set; }
    }
}
