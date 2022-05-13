using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieService.Contexts;
using MovieService.Entities;

namespace MovieService.Repositories
{
    public class UserToMovieRepository : Repository<UserToMovie>
    {
        public UserToMovieRepository() : base(null)
        {
        }
        public UserToMovieRepository(MovieContext context) : base(context)
        {
        }

        public override void Add(UserToMovie item)
        {
            base.Add(item);
            Save();
        }

        public UserToMovie GetOneByUserAndMovieIds(string userId, int movieId)
        {
            return GetAll().FirstOrDefault(x => x.ApplicationUserId == userId && x.MovieId == movieId);
        }
    }
}
