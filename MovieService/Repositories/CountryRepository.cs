﻿using MovieService.Contexts;
using MovieService.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Repositories
{
    public class CountryRepository : Repository<Country>
    {
        public CountryRepository() : base(null) { }

        public CountryRepository(MovieContext context) : base(context) { }

        public Country GetByName(string name)
        {
            return ((MovieContext)_context).Countries.FirstOrDefault(c => c.Name == name);
        }

        /// <summary>
        /// Gets range of countries by names, creates if doesnt exist
        /// </summary>
        /// <param name="countryNames"></param>
        /// <param name="movie"></param>
        /// <returns>list of countries</returns>
        public virtual List<Country> GetRangeByName(List<string> countryNames, Movie movie=null)
        {
            var countries = new List<Country>();
            foreach (var countryName in countryNames)
            {
                var country = GetByName(countryName);
                if (country == null)
                {
                    country = new Country();
                    country.Name = countryName;
                    Add(country);
                    Save();
                }
                if (movie != null)
                    country.Movies.Add(movie);
                countries.Add(country);
            }

            return countries;
        }
    }
}
