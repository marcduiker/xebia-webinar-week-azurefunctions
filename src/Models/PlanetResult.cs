using Newtonsoft.Json;
using System.Collections.Generic;

namespace Xebia.WebinarWeek.Models
{
    public class PlanetResult
    {

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("results")]
        public IEnumerable<Planet> Results { get; set; }
    }
}
