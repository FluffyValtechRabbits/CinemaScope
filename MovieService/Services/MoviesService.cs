using System.Collections.Generic;
using System.Linq;
using MovieService.Dtos;
using MovieService.Entities;
using MovieService.Interfaces;
using MovieService.Interfaces.ServicesInterfaces;

namespace MovieService.Services
{
    public class MoviesService : IMoviesService
    {
        IUnitOfWork _unitOfWork;

        public MoviesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<MostWatchedDto> MostWatched()
        {
            return _unitOfWork.UserToMovieRepository.GetAll()
                .Where(m => m.IsWatched)
                .GroupBy(m => m.MovieId)
                .Select(m =>
                    {
                        var movie = _unitOfWork.MovieRepository.GetById(m.Key);
                        return new MostWatchedDto()
                        {
                            Title = movie.Title,
                            Plot = movie.Plot,
                            Image = movie.Poster,
                            Watched = m.Count()
                        };
                    }
                ).OrderByDescending(m => m.Watched)
                .Take(10)
                .ToList();
        }
        public List<MostLikedDto> MostLiked()
        {
            return _unitOfWork.UserToMovieRepository.GetAll()
                .Where(m => m.IsLiked)
                .GroupBy(m => m.MovieId)
                .Select(m =>
                    {
                        var movie = _unitOfWork.MovieRepository.GetById(m.Key);
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
        public void LikeMovie(string userId, int id)
        {
            var userToMovie = _unitOfWork.UserToMovieRepository.GetOneByUserAndMovieIds(userId, id);
            if (userToMovie is null)
            {
                userToMovie = new UserToMovie()
                {
                    IsDisLiked = false,
                    ApplicationUserId = userId,
                    IsLiked = true,
                    IsWatched = false,
                    MovieId = id
                };
                _unitOfWork.UserToMovieRepository.Add(userToMovie);
            }
            else if (userToMovie.IsLiked)
            {
                userToMovie.IsLiked = false;
                _unitOfWork.UserToMovieRepository.Update(userToMovie);
                if (!userToMovie.IsWatched)
                    _unitOfWork.UserToMovieRepository.Delete(userToMovie);
            }
            else
            {
                if (userToMovie.IsDisLiked)
                    userToMovie.IsDisLiked = false;
                userToMovie.IsLiked = true;
                _unitOfWork.UserToMovieRepository.Update(userToMovie);
            }
        }
        public void DislikeMovie(string userId, int id)
        {
            var userToMovie = _unitOfWork.UserToMovieRepository.GetOneByUserAndMovieIds(userId, id);
            if (userToMovie is null)
            {
                userToMovie = new UserToMovie()
                {
                    IsDisLiked = true,
                    ApplicationUserId = userId,
                    IsLiked = false,
                    IsWatched = false,
                    MovieId = id
                };
                _unitOfWork.UserToMovieRepository.Add(userToMovie);
            }
            else if (userToMovie.IsDisLiked)
            {
                userToMovie.IsDisLiked = false;
                _unitOfWork.UserToMovieRepository.Update(userToMovie);
                if (!userToMovie.IsWatched)
                    _unitOfWork.UserToMovieRepository.Delete(userToMovie);
            }
            else
            {
                if (userToMovie.IsLiked)
                    userToMovie.IsLiked = false;
                userToMovie.IsDisLiked = true;
                _unitOfWork.UserToMovieRepository.Update(userToMovie);
            }
        }
        public void MarkAsWatched(string userId, int id)
        {
            var userToMovie = _unitOfWork.UserToMovieRepository.GetOneByUserAndMovieIds(userId, id);

            if (userToMovie is null)
            {
                userToMovie = new UserToMovie()
                {
                    IsDisLiked = false,
                    ApplicationUserId = userId,
                    IsLiked = false,
                    IsWatched = true,
                    MovieId = id
                };
                _unitOfWork.UserToMovieRepository.Add(userToMovie);
            }
            else if (userToMovie.IsWatched)
            {
                userToMovie.IsWatched = false;
                _unitOfWork.UserToMovieRepository.Update(userToMovie);
                if (!userToMovie.IsDisLiked && !userToMovie.IsLiked)
                    _unitOfWork.UserToMovieRepository.Delete(userToMovie);
            }
            else
            {
                userToMovie.IsWatched = true;
                _unitOfWork.UserToMovieRepository.Update(userToMovie);
            }
        }

        public string GetUserRating(int id)
        {
            var allLikes = _unitOfWork.UserToMovieRepository.GetAll().Count(x => x.IsLiked&&x.MovieId==id);
            var allDislikes = _unitOfWork.UserToMovieRepository.GetAll().Count(x => x.IsDisLiked&&x.MovieId==id);
            return (allDislikes + allLikes) == 0 ? "0.0" : (10*allLikes/(allDislikes+allLikes)).ToString("0.0");
        }

    }
}
