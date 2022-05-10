using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieService.Entities;

namespace MovieService.Contexts
{
    public class MovieContext : DbContext
    {
        DbSet<MovieType> Types { get; set; }

        DbSet<Movie> Movies { get; set; }

        DbSet<Country> Countries { get; set; }

        DbSet<Genre> Genres { get; set; }

        public MovieContext() : base("MovieDbContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
