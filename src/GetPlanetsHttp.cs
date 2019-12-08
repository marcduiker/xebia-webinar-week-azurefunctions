using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Xebia.WebinarWeek
{
    public class GetPlanetsHttp
    {
        private readonly StarWarsPlanetProvider _planetProvider = new StarWarsPlanetProvider();
        
        [FunctionName(nameof(GetPlanetsHttp))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "planets")] HttpRequest req,
            ILogger log)
        {
            IActionResult functionResult;
            try
            {
                var planets = await _planetProvider.GetPlanets();
                functionResult = new OkObjectResult(planets);
            }
            catch (HttpRequestException e)
            {
                functionResult = new ObjectResult(e.Message) { StatusCode = 500 };
            }

            return functionResult;
        }
    }
}
