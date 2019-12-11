using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Xebia.WebinarWeek.Planets
{
    public class StarWarsPlanetProvider
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<Planet>> GetPlanets()
        {
            var planetProviderUri = Environment.GetEnvironmentVariable("PlanetProviderUri");

            return await GetPlanets(planetProviderUri);
        }

        private async Task<List<Planet>> GetPlanets(string planetProviderUri)
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
