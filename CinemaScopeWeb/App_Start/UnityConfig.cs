using MovieService.Contexts;
using MovieService.Imdb;
using MovieService.Interfaces;
using MovieService.Repositories;
using MovieService.Services;
using System.Web.Mvc;
using MovieService.Interfaces.ServicesInterfaces;
using MovieService.UOW;
using Unity;
using Unity.Mvc5;
using UserService.Interfaces;
using UserService.Services;

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
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IUserService, UserService.Services.UserService>();
            container.RegisterType<IMoviesService, MoviesService>();
            container.RegisterType<IImdbService, ImdbService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}