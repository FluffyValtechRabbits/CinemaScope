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
using MovieService.Interfaces.ServiceInterfaces;

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
            container.RegisterType<IMovieService, MovieService.Services.MovieService>();
            container.RegisterType<IImdbService, ImdbService>();
            container.RegisterType<MovieService.Interfaces.IUnitOfWork, 
                                   MovieService.UOW.UnitOfWork>();
            container.RegisterType<UserService.Interfaces.IUnitOfWork,
                                   UserService.UOW.UnitOfWork>();
            container.RegisterType<IUserStatsService, UserStatsService>();
            container.RegisterType<IAboutUsService, AboutUsService>();
            container.RegisterType<IFilteringService, FilteringService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}