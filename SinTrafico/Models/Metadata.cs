using System;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class Metadata
    {
        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("segment_id")]
        public int SegmentId { get; set; }

        [JsonProperty("cliente")]
        public string Cliente { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
