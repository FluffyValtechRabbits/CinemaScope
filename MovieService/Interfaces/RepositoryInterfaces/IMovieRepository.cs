using MovieService.Entities;

namespace MovieService.Interfaces.RepositoryInterfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void CreateUpdate(Movie movie);
        Movie GetLastUploaded();

        Movie GetByImdbId(string ImdbId);
    }
}
