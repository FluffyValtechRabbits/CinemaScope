﻿using MovieService.Contexts;
using MovieService.Entities;
using System.Data.Entity;
using System.Linq;
using MovieService.Interfaces.RepositoryInterfaces;
using System.Collections.Generic;

namespace MovieService.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieContext context=null) : base(context) { }

        public List<Movie> GetAllNewestFirst()
        {
            return ((MovieContext)_context).Movies.OrderByDescending(m => m.Year).ToList();
        }

        public void CreateUpdate(Movie movie)
        {
            var oldMovie = ((MovieContext)_context).Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (oldMovie != null)
            {
                movie.Id = oldMovie.Id;
                oldMovie = movie;
                Update(oldMovie);
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

        public override void Delete(Movie item)
        {
            var repository = new Repository<UserToMovie>(_context);
            var movies = repository.GetAll().Where(um => um.MovieId == item.Id);
            foreach (var movie in movies)
            {
                repository.Delete(movie);
            }
            Save();
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
            Save();
            base.DeleteById(id);
        }

        public Movie GetLastUploadedFromImdb()
        {
            return ((MovieContext)_context).Movies.OrderByDescending(m => m.Id).Where(m => m.ImdbId != null).FirstOrDefault();
        }

        public Movie GetByImdbId(string ImdbId)
        {
            if (string.IsNullOrEmpty(ImdbId)) { return null; }

            return ((MovieContext)_context).Movies.FirstOrDefault(m => m.ImdbId == ImdbId);
        }
    }
}
