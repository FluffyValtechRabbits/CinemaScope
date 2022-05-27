using MovieService.Entities;
using System.Collections.Generic;

namespace MovieService.Interfaces.RepositoryInterfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        List<Movie> GetAllNewestFirst();
        void CreateUpdate(Movie movie);
        Movie GetLastUploadedFromImdb();
        Movie GetByImdbId(string ImdbId);

        Movie GetByName(string title);
    }
}
