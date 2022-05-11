﻿using System.Data.Entity;
using MovieService.Entities;

namespace MovieService.Contexts
{
    public class MovieContext : DbContext
    {
        public DbSet<MovieType> Types { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<UserToMovie> UserToMovies { get; set; }

        public MovieContext() : base("MovieDbContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserToMovie>().HasKey(k => new {k.ApplicationUserId, k.MovieId});
            base.OnModelCreating(modelBuilder);
        }
    }
}
