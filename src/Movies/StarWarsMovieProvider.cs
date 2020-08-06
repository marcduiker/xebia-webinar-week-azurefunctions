using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Xebia.WebinarWeek.Movies
{
    public class StarWarsMovieProvider : IMovieProvider
    {
        private readonly HttpClient _httpClient;

        public StarWarsMovieProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<List<Movie>> GetMovies()
        {
            var movieProviderUrl = Environment.GetEnvironmentVariable("MovieProviderUri");

            return await GetMovies(movieProviderUrl);
        }

        private async Task<List<Movie>> GetMovies(string movieProviderUrl)
        {

            var planets = new List<Movie>();
            var responseMessage = await _httpClient.GetAsync(movieProviderUrl);
            if (responseMessage.IsSuccessStatusCode)
            {
                var movieResult = await responseMessage.Content.ReadAsAsync<MovieResult>();
                planets.AddRange(movieResult.Results);
                if (!string.IsNullOrEmpty(movieResult.Next))
                {
                    planets.AddRange(await GetMovies(movieResult.Next));
                }
            }
            else
            {
                throw new HttpRequestException(responseMessage.ReasonPhrase);
            }

            return planets;
        }
    }
}
