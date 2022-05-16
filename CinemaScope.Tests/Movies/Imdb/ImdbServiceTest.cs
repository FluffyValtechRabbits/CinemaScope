using Moq;
using MovieService.Entities;
using MovieService.Imdb;
using MovieService.Repositories;
using NUnit.Framework;
using Services;
using System.Collections.Generic;

namespace CinemaScope.Tests.Movies.Imdb
{
    [TestFixture]
    public class ImdbServiceTest
    {
        private Mock<MovieTypeRepository> mockMovieTypeRepo;
        private Mock<GenreRepository> mockGenreRepo;
        private Mock<CountryRepository> mockCountryRepo;
        private Mock<CustomHttpClient> mockHttpClient;
        private ImdbService testService;

       [SetUp]
        public void SetUp()
        {
            mockMovieTypeRepo = new Mock<MovieTypeRepository>();
            mockGenreRepo = new Mock<GenreRepository>();
            mockCountryRepo = new Mock<CountryRepository>();
            mockHttpClient = new Mock<CustomHttpClient>();
            testService = new ImdbService(mockMovieTypeRepo.Object, mockGenreRepo.Object, mockCountryRepo.Object, mockHttpClient.Object);
        }

        [Test]
        public void SearchMovie_DefaultScenario_ReturnsSearchOptionList()
        {
            var searchOption = "inception 2010";
            var objectsInResponse = 5;
            mockHttpClient.Setup(x => x.GetJson(It.IsAny<string>())).Returns(Imdb.FakeApiResponses.FakeMovieSearchResponse);

            var results = testService.SearchMovie(searchOption);

            Assert.That(results, Is.Not.Empty);
            Assert.That(results.Count, Is.EqualTo(objectsInResponse));
        }

        [TestCase("")]
        [TestCase(null)]
        public void SearchMovie_NullorEmptyStringPassed_ReturnsNull(string searchOption)
        {
            Assert.IsNull(testService.SearchMovie(searchOption));
        }

        [Test]
        public void GetMovieByid_DefaultScenario_ReturnsMovieModel()
        {
            var mockMovieId = "tt0411008";
            var mockMovieTypeId = 1;
            mockMovieTypeRepo.Setup(x => x.GetByName(It.IsAny<string>())).Returns(mockMovieTypeId);
            var mockGenres = new List<Genre>() { new Genre() { Name = "Adventure" }, new Genre() { Name = "Drama" }, new Genre() { Name = "Fantasy" } };
            mockGenreRepo.Setup(x => x.GetRangeByName(It.IsAny<List<string>>(), It.IsAny<Movie>())).Returns(mockGenres);
            var mockCountries = new List<Country>() { new Country() { Name = "USA" } };
            mockCountryRepo.Setup(x => x.GetRangeByName(It.IsAny<List<string>>(), It.IsAny<Movie>())).Returns(mockCountries);
            mockHttpClient.Setup(x => x.GetJson(It.IsAny<string>())).Returns(FakeApiResponses.FakeMovieByIdResponse);

            var movie = testService.GetMovieByImdbId(mockMovieId);

            Assert.NotNull(movie);
            Assert.That(movie.Title == "Lost");
            Assert.That(movie.Year == "2004");
            Assert.That(movie.Countries == mockCountries);
            Assert.That(movie.Genres == mockGenres);
            Assert.That(movie.TypeId == mockMovieTypeId);
        }

        [TestCase(null)]
        [TestCase("")]
        public void GetMovieById_PassedNullOrEmptyString_ReturnsNull(string movieId)
        {
            Assert.IsNull(testService.GetMovieByImdbId(movieId));
        }
    }
}
