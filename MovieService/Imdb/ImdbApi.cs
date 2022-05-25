﻿namespace MovieService.Imdb
{
    public static class ImdbApi
    {
        public static string apiKey = "k_i4gclk97";
        public static string searchRequest = "https://imdb-api.com/en/API/Search/{0}/{1}";
        public static string movieRequest = "https://imdb-api.com/en/API/Title/{0}/{1}/FullActor,FullCast,Posters,Images,Ratings,";
        public static string top250Request = "https://imdb-api.com/en/API/Top250Movies/{0}";
        public static string MoiveIdCode = "tt";
        public static string MovieIdStartNumber = "0311992";
        public static int MaxMovieUpdateTries = 20;
        public static int MaxMovieUpdates = 8;
    }
}
