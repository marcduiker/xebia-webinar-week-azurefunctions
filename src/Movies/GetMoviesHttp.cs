using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Linq;
using System;

namespace Xebia.WebinarWeek.Movies
{
    public class GetMoviesHttp
    {
        private readonly IMovieProvider _movieProvider;

        public GetMoviesHttp(IMovieProvider movieProvider)
        {
            _movieProvider = movieProvider;
        }
        
        [FunctionName(nameof(GetMoviesHttp))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "movies")] HttpRequest req,
            ILogger log)
        {
            IActionResult functionResult;
            try
            {
                var movies = await _movieProvider.GetMovies();
                var orderedMovies = movies.OrderBy(m => m.Episode);
                functionResult = new OkObjectResult(orderedMovies);
            }
            catch (Exception e)
            {
                functionResult = new ObjectResult(e.Message) { StatusCode = 500 };
            }

            return functionResult;
        }
    }
}
