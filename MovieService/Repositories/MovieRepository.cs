using MovieService.Contexts;
using MovieService.Entities;
using System.Data.Entity;
using System.Linq;
using MovieService.Interfaces.RepositoryInterfaces;

namespace MovieService.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieContext context) : base(context) { }

        public void CreateUpdate(Movie movie)
        {
            var oldMovie = ((MovieContext)_context).Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (oldMovie != null)
            {
                oldMovie.Title = movie.Title;
                oldMovie.Year = movie.Year;
                oldMovie.Poster = movie.Poster;
                oldMovie.Plot = movie.Plot;
                oldMovie.Countries.Clear();
                oldMovie.Countries = movie.Countries;
                oldMovie.Genres.Clear();
                oldMovie.Genres = movie.Genres;
                oldMovie.Budget = movie.Budget;
                oldMovie.BoxOffice = movie.BoxOffice;
                oldMovie.Cast = movie.Cast;
                oldMovie.TypeId = movie.TypeId;
            } else
            {
                ((MovieContext)_context).Movies.Add(movie);
            }
            Save();
        }

        public override Movie GetById(int id)
        {
            var movie = ((MovieContext)_context).Movies.
                Where(m => m.Id == id).Include((m => m.Genres)).Include(m => m.Countries).FirstOrDefault();

            return movie;
        }
    }
}
