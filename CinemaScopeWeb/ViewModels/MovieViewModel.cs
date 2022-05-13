using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieService.Entities;

namespace CinemaScopeWeb.ViewModels
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }
        public bool IsWatched { get; set; }
    }
}