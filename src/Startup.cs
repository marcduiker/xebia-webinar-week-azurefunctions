using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xebia.WebinarWeek;
using Xebia.WebinarWeek.Movies;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Xebia.WebinarWeek
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddTransient<IMovieProvider, StarWarsMovieProvider>();
        }
    }
}
