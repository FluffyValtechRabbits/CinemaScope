using MovieService.Contexts;
using MovieService.Imdb;
using MovieService.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

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
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}