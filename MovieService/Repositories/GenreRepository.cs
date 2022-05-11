using MovieService.Contexts;
using MovieService.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MovieService.Repositories
{
    public class GenreRepository : Repository<Genre>
    {
        public GenreRepository() : base(null) { }

        public GenreRepository(MovieContext context) : base(context) { }
        
        private Genre GetByName(string name)
        {
            return ((MovieContext)_context).Genres.FirstOrDefault(g => g.Name == name);
        }

        /// <summary>
        /// Gets range of genres by given names, creates if dont exist
        /// </summary>
        /// <param name="genreNames"></param>
        /// <returns>list of genres</returns>
        public virtual List<Genre> GetRangeByName(List<string> genreNames, Movie movie=null)
        {
            if (genreNames == null || genreNames.Count == 0)
                return null;

            var genres = new List<Genre>();
            foreach (var name in genreNames)
            {
                var genre = GetByName(name);
                if (genre == null)
                {
                    genre = new Genre();
                    genre.Name = name;
                    Add(genre);
                    Save();
                }
                if (movie != null)
                    genre.Movies.Add(movie);
                genres.Add(genre);
            }

            return genres;
        }
    }
}
