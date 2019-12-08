using Newtonsoft.Json;

namespace Xebia.WebinarWeek.Planets
{
    public class Planet
    {
        [JsonProperty("climate")]
        public string Climate { get; set; }
        
        [JsonProperty("gravity")]
        public string Gravity { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("population")]
        public string Population { get; set; }

        [JsonProperty("terrain")]
        public string Terrain { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}