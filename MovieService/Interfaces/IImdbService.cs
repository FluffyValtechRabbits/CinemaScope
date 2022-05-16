using Imdb;
using IMDbApiLib.Models;
using MovieService.Entities;
using System.Collections.Generic;

namespace MovieService.Interfaces
{
    public interface IImdbService
    {
        List<SearchOption> SearchMovie(string searchOption);
        Movie GetMovieByImdbId(string movieId);
        Top250Data GetTop250();
    }
}
