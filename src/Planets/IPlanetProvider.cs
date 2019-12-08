using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xebia.WebinarWeek.Planets
{
    public interface IPlanetProvider
    {
        Task<List<Planet>> GetPlanets();
    }
}