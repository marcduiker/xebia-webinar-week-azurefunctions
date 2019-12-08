using Newtonsoft.Json;
using System.Collections.Generic;

namespace Xebia.WebinarWeek.Movies
{
    public class MovieResult
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("results")]
        public IEnumerable<Movie> Results { get; set; }
    }
}
