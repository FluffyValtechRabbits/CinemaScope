using MovieService.ViewModels;
using System.Collections.Generic;

namespace MovieService.Interfaces
{
    public interface IRatingService
    {
        List<MostWatchedViewModel> MostWatched();
        List<MostLikedViewModel> MostLiked();
    }
}
