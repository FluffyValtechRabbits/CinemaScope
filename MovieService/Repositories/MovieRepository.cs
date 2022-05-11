using MovieService.Contexts;
using MovieService.Entities;
using System.Data.Entity;
using System.Linq;

namespace MovieService.Repositories
{
    public class MovieRepository : Repository<Movie>
    {
        public MovieRepository(MovieContext context) : base(context) { }

        public override Movie GetById(int id)
        {
            var movie = ((MovieContext)_context).Movies.
                Where(m => m.Id == id).Include((m => m.Genres)).Include(m => m.Countries).FirstOrDefault();

            return movie;
        }
    }
}
