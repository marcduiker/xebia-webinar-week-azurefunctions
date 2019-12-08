using Newtonsoft.Json;

namespace Xebia.WebinarWeek.Movies
{
    public class Movie
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("episode_id")]
        public string Episode { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
    }
}