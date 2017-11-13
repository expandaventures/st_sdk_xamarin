using System;
using Newtonsoft.Json;

namespace SinTrafico
{
    public class Properties
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
