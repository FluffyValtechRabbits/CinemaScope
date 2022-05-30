using Moq;
using MovieService.Entities;
using MovieService.Imdb;
using MovieService.Interfaces;
using MovieService.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CinemaScope.Tests.Movies.Imdb
{
    [TestFixture]
    public class ImdbServiceTest
    {
        private Mock<IUnitOfWork> mockUnitOfWork;
        private Mock<CustomHttpClient> mockHttpClient;
        private ImdbService testService;

       [SetUp]
        public void SetUp()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockHttpClient = new Mock<CustomHttpClient>();
            testService = new ImdbService(mockUnitOfWork.Object, mockHttpClient.Object);
        }

        [Test]
        public void GetMovieByid_DefaultScenario_AddsNewMovieModelReturnsTrue()
        {
            Movie model = null;
            var mockMovieId = "tt0411008";
            mockHttpClient.Setup(x => x.GetJson(It.IsAny<string>())).Returns(FakeApiResponses.FakeMovieByIdResponse);
            mockUnitOfWork.Setup(x => x.MovieRepository.Add(It.IsAny<Movie>())).Callback((Movie m) => { model = m; });
            mockUnitOfWork.Setup(x => x.GenreRepository.GetRangeByName(It.IsAny<List<string>>())).Callback(() => { });
            mockUnitOfWork.Setup(x => x.CountryRepository.GetRangeByName(It.IsAny<List<string>>())).Callback(() => { });

            var result = testService.GetMovieByImdbId(mockMovieId);

            Assert.IsTrue(result);
            Assert.NotNull(model);
            Assert.That(model.ImdbId == mockMovieId);
        }

        [TestCase(null)]
        [TestCase("")]
        public void GetMovieById_PassedNullOrEmptyString_ReturnsFalse(string movieId)
        {
            Assert.IsFalse(testService.GetMovieByImdbId(movieId));
        }
    }
}
