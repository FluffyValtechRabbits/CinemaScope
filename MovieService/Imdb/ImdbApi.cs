﻿namespace MovieService.Imdb
{
    public static class ImdbApi
    {
        public static string apiKey = "k_30gw6nbe";
        public static string searchRequest = "https://imdb-api.com/en/API/Search/{0}/{1}";
        public static string movieRequest = "https://imdb-api.com/en/API/Title/{0}/{1}/FullActor,FullCast,Posters,Images,Ratings,";
    }
}