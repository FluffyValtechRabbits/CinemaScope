using MovieService.Contexts;
using MovieService.Entities;

namespace MovieService.Repositories
{
    public class UserToMovieRepository : Repository<UserToMovie>
    {
        public UserToMovieRepository(MovieContext context) : base(context) { }
    }
}
