using MovieService.Interfaces;
using MovieService.Repositories;
using MovieService.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MovieService.Services
{
    public class RatingService : IRatingService
    {
        private UserToMovieRepository _userMovieRepository;
        private MovieRepository _movieRepository;

        public RatingService(UserToMovieRepository userMovieRepo, MovieRepository movieRepo) {
            _userMovieRepository = userMovieRepo; 
            _movieRepository = movieRepo;
        }

        public List<MostWatchedViewModel> MostWatched()
        {
            return _userMovieRepository.GetAll().Where(m => m.IsWatched == true).GroupBy(m => m.MovieId).Select(m =>
            {
                var movie = _movieRepository.GetById(m.Key);
                return new MostWatchedViewModel() { Title = movie.Title, Plot = movie.Plot, Image = movie.Poster, Watched = m.Count()};
            }
            ).OrderByDescending(m => m.Watched).Take(10).ToList();
        }

        public List<MostLikedViewModel> MostLiked()
        {
            return _userMovieRepository.GetAll().Where(m => m.IsLiked == true).GroupBy(m => m.MovieId).Select(m =>
            {
                var movie = _movieRepository.GetById(m.Key);
                return new MostLikedViewModel() { Title = movie.Title, Plot = movie.Plot, Image = movie.Poster, Liked = m.Count() };
            }
            ).OrderByDescending(m => m.Liked).Take(10).ToList();
        }
    }
}
