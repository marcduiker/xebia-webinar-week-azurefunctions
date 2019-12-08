using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xebia.WebinarWeek.Models;

namespace Xebia.WebinarWeek
{
    public class StarWarsPlanetProvider
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<Planet>> GetPlanets(string planetProviderUri = "https://swapi.co/api/planets")
        {
            var planets = new List<Planet>();
            var responseMessage = await _httpClient.GetAsync(planetProviderUri);
            if (responseMessage.IsSuccessStatusCode)
            {
                var planetResult = await responseMessage.Content.ReadAsAsync<PlanetResult>();
                planets.AddRange(planetResult.Results);
                if (!string.IsNullOrEmpty(planetResult.Next))
                {
                    planets.AddRange(await GetPlanets(planetResult.Next));
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
