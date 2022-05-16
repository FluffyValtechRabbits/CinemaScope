using System.Collections.Generic;
using MovieService.Dtos;

namespace MovieService.Interfaces
{
    public interface IRatingService
    {
        List<MostWatchedDto> MostWatched();
        List<MostLikedDto> MostLiked();
    }
}
