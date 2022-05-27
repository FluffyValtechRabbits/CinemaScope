using Moq;
using MovieService.Interfaces;
using NUnit.Framework;
using MovieService.Entities;

namespace CinemaScope.Tests.Movies.Services
{
    [TestFixture]
    public class MovieServiceTest
    {
        [Test]
        public void LikeMovieTest_DefaultScenario_MakesMovieLiked()
        {
            var testModel = new UserToMovie() { IsDisLiked = false, IsWatched = true, IsLiked = false, ApplicationUserId = "1", MovieId = 1 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Update(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.LikeMovie("1", 1);

            Assert.That(testModel.IsLiked == true);
        }

        [Test]
        public void LikeMovieTest_DislikedMovie_RemovesDislikePlacesLike()
        {
            var testModel = new UserToMovie() { IsDisLiked = false, IsWatched = true, IsLiked = false, ApplicationUserId = "1", MovieId = 1 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Update(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.LikeMovie("1", 1);

            Assert.That(testModel.IsLiked == true);
            Assert.That(testModel.IsDisLiked == false);
        }

        [Test]
        public void LikeMovieTest_LikedMovie_RemovesLike()
        {
            var testModel = new UserToMovie() { IsDisLiked = false, IsWatched = true, IsLiked = true, ApplicationUserId = "1", MovieId = 1 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Update(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.LikeMovie("1", 1);

            Assert.That(testModel.IsLiked == false);
        }

        [Test]
        public void LikeMovieTest_UnwatchedLikedMovie_RemovesMovieFromTable()
        {
            var testModel = new UserToMovie() { IsDisLiked = false, IsWatched = false, IsLiked = true, ApplicationUserId = "1", MovieId = 1 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Update(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            mockUnitOfWork.Setup(M => M.UserToMovieRepository.Delete(It.IsAny<UserToMovie>()));
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.LikeMovie("1", 1);

            Assert.That(!testModel.IsLiked);
            Assert.DoesNotThrow(() => mockUnitOfWork.Verify(mock => mock.UserToMovieRepository.Delete(It.IsAny<UserToMovie>()), Times.Once()));
        }

        [Test]
        public void LikeMovieTest_UserNeverContactedMovie_CreatesNewRecordInTable()
        {
            UserToMovie testModel = null;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Add(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            mockUnitOfWork.Setup(M => M.UserToMovieRepository.Delete(It.IsAny<UserToMovie>()));
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.LikeMovie("1", 1);

            Assert.IsNotNull(testModel);
            Assert.That(testModel.IsLiked);
        }

        [Test]
        public void DisLikeMovieTest_DefaultScenario_MakesMovieLiked()
        {
            var testModel = new UserToMovie() { IsDisLiked = false, IsWatched = true, IsLiked = false, ApplicationUserId = "1", MovieId = 1 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Update(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.DislikeMovie("1", 1);

            Assert.That(testModel.IsDisLiked);
        }

        [Test]
        public void DisLikeMovieTest_DislikedMovie_RemovesDislikePlacesLike()
        {
            var testModel = new UserToMovie() { IsDisLiked = false, IsWatched = true, IsLiked = true, ApplicationUserId = "1", MovieId = 1 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Update(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.DislikeMovie("1", 1);

            Assert.That(testModel.IsDisLiked);
            Assert.That(!testModel.IsLiked);
        }

        [Test]
        public void DisLikeMovieTest_LikedMovie_RemovesDisLike()
        {
            var testModel = new UserToMovie() { IsDisLiked = true, IsWatched = true, IsLiked = false, ApplicationUserId = "1", MovieId = 1 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Update(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.DislikeMovie("1", 1);

            Assert.That(!testModel.IsDisLiked);
        }

        [Test]
        public void DisLikeMovieTest_UnwatchedDisLikedMovie_RemovesMovieFromTable()
        {
            var testModel = new UserToMovie() { IsDisLiked = true, IsWatched = false, IsLiked = false, ApplicationUserId = "1", MovieId = 1 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Update(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            mockUnitOfWork.Setup(M => M.UserToMovieRepository.Delete(It.IsAny<UserToMovie>()));
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.DislikeMovie("1", 1);

            Assert.That(!testModel.IsDisLiked);
            Assert.DoesNotThrow(() => mockUnitOfWork.Verify(mock => mock.UserToMovieRepository.Delete(It.IsAny<UserToMovie>()), Times.Once()));
        }

        [Test]
        public void DisLikeMovieTest_UserNeverContactedMovie_CreatesNewRecordInTable()
        {
            UserToMovie testModel = null;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Add(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            mockUnitOfWork.Setup(M => M.UserToMovieRepository.Delete(It.IsAny<UserToMovie>()));
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.DislikeMovie("1", 1);

            Assert.IsNotNull(testModel);
            Assert.That(testModel.IsDisLiked);
        }

        [Test]
        public void MarkAsWatchedTest_UnmarksWatchedMovie()
        {
            var testModel = new UserToMovie() { IsDisLiked = false, IsWatched = true, IsLiked = false, ApplicationUserId = "1", MovieId = 1 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Update(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Delete(It.IsAny<UserToMovie>()));
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.MarkAsWatched("1", 1);

            Assert.That(!testModel.IsWatched);
            Assert.DoesNotThrow(() => mockUnitOfWork.Verify(mock => mock.UserToMovieRepository.Delete(It.IsAny<UserToMovie>()), Times.Once()));
        }

        [Test]
        public void MarkAsWatchedTest_DefaultScenario_MarksWatched()
        {
            var testModel = new UserToMovie() { IsDisLiked = false, IsWatched = false, IsLiked = false, ApplicationUserId = "1", MovieId = 1 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Update(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.MarkAsWatched("1", 1);

            Assert.That(testModel.IsWatched);
        }

        [Test]
        public void MarkAsWatchedTest_UserNeverContactedMovie_CreatesNewRecordInTable()
        {
            UserToMovie testModel = null;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.GetOneByUserAndMovieIds(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(testModel);
            mockUnitOfWork.Setup(m => m.UserToMovieRepository.Add(It.IsAny<UserToMovie>()))
                .Callback((UserToMovie model) => { testModel = model; });
            mockUnitOfWork.Setup(M => M.UserToMovieRepository.Delete(It.IsAny<UserToMovie>()));
            var testService = new MovieService.Services.MovieService(mockUnitOfWork.Object);

            testService.MarkAsWatched("1", 1);

            Assert.IsNotNull(testModel);
            Assert.That(testModel.IsWatched);
        }
    }
}
