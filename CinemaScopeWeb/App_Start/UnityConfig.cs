using MovieService.Contexts;
using MovieService.Imdb;
using MovieService.Repositories;
using UserService.Managers;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using UserService.Models;
using UserService;

namespace CinemaScopeWeb
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<MovieContext>();
            container.RegisterType<MovieTypeRepository>();
            container.RegisterType<GenreRepository>();
            container.RegisterType<CountryRepository>();
            container.RegisterType<CustomHttpClient>();
            container.RegisterType<MovieRepository>();        
            container.RegisterType<UserToMovieRepository>();        
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}