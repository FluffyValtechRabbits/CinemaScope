using MovieService.Interfaces;
using MovieService.Repositories;
using System.Collections.Generic;
using System.Linq;
using MovieService.Dtos;

namespace MovieService.Services
{
    public class RatingService : IRatingService
    {
        private UserToMovieRepository _userMovieRepository;
        private MovieRepository _movieRepository;

        public RatingService(UserToMovieRepository userMovieRepo, MovieRepository movieRepo) 
        {
            _userMovieRepository = userMovieRepo; 
            _movieRepository = movieRepo;
        }

        public List<MostWatchedDto> MostWatched()
        {
            return _userMovieRepository.GetAll()
                .Where(m => m.IsWatched)
                .GroupBy(m => m.MovieId)
                .Select(m =>
            {
                var movie = _movieRepository.GetById(m.Key);
                return new MostWatchedDto() { Title = movie.Title, Plot = movie.Plot, Image = movie.Poster, Watched = m.Count()};
            }
            ).OrderByDescending(m => m.Watched)
                .Take(10)
                .ToList();
        }

        public List<MostLikedDto> MostLiked()
        {
            return _userMovieRepository.GetAll()
                .Where(m => m.IsLiked)
                .GroupBy(m => m.MovieId)
                .Select(m =>
            {
                var movie = _movieRepository.GetById(m.Key);
                return new MostLikedDto()
                {
                    Title = movie.Title, 
                    Plot = movie.Plot, 
                    Image = movie.Poster, 
                    Liked = m.Count()
                };
            }
            ).OrderByDescending(m => m.Liked)
                .Take(10)
                .ToList();
        }
    }
}
