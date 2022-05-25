using System.Collections.Generic;
using Imdb;
using IMDbApiLib.Models;
using MovieService.Entities;

namespace MovieService.Interfaces.ServiceInterfaces
{
    public interface IImdbService
    {
        List<SearchOption> SearchMovie(string searchOption);
        Movie GetMovieByImdbId(string movieId);
        Top250Data GetTop250();
    }
}
