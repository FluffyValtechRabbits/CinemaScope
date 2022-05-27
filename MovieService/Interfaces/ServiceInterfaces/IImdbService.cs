using IMDbApiLib.Models;

namespace MovieService.Interfaces.ServiceInterfaces
{
    public interface IImdbService
    {
        bool GetMovieByImdbId(string movieId);
        Top250Data GetTop250();
    }
}
