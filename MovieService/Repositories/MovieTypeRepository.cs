using MovieService.Contexts;
using MovieService.Entities;
using System;
using System.Linq;

namespace MovieService.Repositories
{
    public class MovieTypeRepository : Repository<MovieType>
    {
        public MovieTypeRepository() : base(null) { }

        public MovieTypeRepository(MovieContext context) : base(context) { }

        /// <summary>
        /// Gets movie type id by name, creates if doesnt exist
        /// </summary>
        /// <param name="movieTypeName"></param>
        /// <returns>id of movie type</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual int GetByName(string movieTypeName)
        {
            if (string.IsNullOrEmpty(movieTypeName))
                throw new ArgumentNullException("movieTypeName can't be null/empty string!");

            var movieType = GetAll().Where(t => t.Name == movieTypeName).SingleOrDefault();
            if (movieType == null)
            {
                movieType = new MovieType();
                movieType.Name = movieTypeName;
                Add(movieType);
                Save();
            }
            return movieType.Id;
        }
    }
}
