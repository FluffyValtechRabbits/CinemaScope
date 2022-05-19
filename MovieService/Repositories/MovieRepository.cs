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

        public override Movie GetById(int id)
        {
            var movie = ((MovieContext)_context).Movies.
                Where(m => m.Id == id).Include((m => m.Genres)).Include(m => m.Countries).FirstOrDefault();

            return movie;
        }

        public override void Delete(Movie item)
        {
            var repository = new Repository<UserToMovie>(_context);
            var movies = repository.GetAll().Where(um => um.MovieId == item.Id);
            foreach (var movie in movies)
            {
                repository.Delete(movie);
            }
            base.Delete(item);
        }

        public override void DeleteById(int id)
        {
            var repository = new Repository<UserToMovie>(_context);
            var movies = repository.GetAll().Where(um => um.MovieId == id);
            foreach (var movie in movies)
            {
                repository.Delete(movie);
            }
            base.DeleteById(id);
        }
    }
}
