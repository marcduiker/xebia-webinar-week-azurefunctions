using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xebia.WebinarWeek.Movies
{
    public interface IMovieProvider
    {
        Task<List<Movie>> GetMovies();
    }
}