using System.Collections.Generic;
using MovieService.Dtos;
using MovieService.Entities;

namespace MovieService.Interfaces.ServicesInterfaces
{
    public interface IMoviesService
    {
        List<MostWatchedDto> MostWatched();
        List<MostLikedDto> MostLiked();
        void LikeMovie(string userId, int id);
        void DislikeMovie(string userId, int id);
        void MarkAsWatched(string userId, int id);
        string GetUserRating(int id);
    }
}
