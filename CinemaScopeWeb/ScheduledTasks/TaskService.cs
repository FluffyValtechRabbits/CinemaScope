using MovieService.Imdb;
using MovieService.Interfaces;
using MovieService.Interfaces.ServiceInterfaces;
using Quartz;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace CinemaScopeWeb.ScheduledTasks
{
    public class TaskService : IJob
    {
        public static readonly string SchedulingStatus = ConfigurationManager.AppSettings["TaskService"];
        private IUnitOfWork _unitOfWork;
        private IImdbService _imdbService;

        public TaskService(IUnitOfWork unitOfWork, IImdbService imdbService) { _unitOfWork = unitOfWork; _imdbService = imdbService; }

        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                if (SchedulingStatus.Equals("ON"))
                {
                    //string path = "C:\\Sample.txt";
                    //using (StreamWriter writer = new StreamWriter(path, true))
                    //{
                    //    writer.WriteLine(string.Format("Quartz Schedular Called on " + DateTime.Now.ToString("dd /MM/yyyy hh:mm:ss tt")));
                    //    writer.Close();
                    //}
                    //string newMovieId;
                    //var lastLoadedMovie = _unitOfWork.MovieRepository.GetLastUploaded();
                    //if (lastLoadedMovie != null)
                    //{
                    //    var lastLoadedId = lastLoadedMovie.ImdbId;
                    //    var lastLoadedIdNumber = int.Parse(lastLoadedId.Replace(ImdbApi.MoiveIdCode, ""));
                    //    lastLoadedIdNumber += 1;
                    //    var newMovieNumber = lastLoadedIdNumber.ToString()
                    //    .PadLeft(ImdbApi.MovieIdStartNumber.Length - lastLoadedIdNumber.ToString().Length, '0');
                    //    newMovieId = ImdbApi.MoiveIdCode + newMovieNumber;
                    //}
                    //else
                    //{
                    //    newMovieId = ImdbApi.MoiveIdCode + ImdbApi.MovieIdStartNumber;
                    //}
                    //_imdbService.GetMovieByImdbId(newMovieId);
                    //_imdbService.GetMovieByImdbId("tt0187393");

                    Update();
                }
            });
            return task;
        }

        public void Update()
        {
            for (int i = 0; i < 3; i++)
            {
                string newMovieId;
                var lastLoadedMovie = _unitOfWork.MovieRepository.GetLastUploaded();
                if (lastLoadedMovie != null)
                {
                    newMovieId = AddId(lastLoadedMovie.ImdbId);
                }
                else
                {
                    newMovieId = ImdbApi.MoiveIdCode + ImdbApi.MovieIdStartNumber;
                }
                var result = AddNewMovie(newMovieId);
                //var counter = 0;
                if (!result)
                {
                    newMovieId = AddId(newMovieId);
                    result = AddNewMovie(newMovieId);
                    //counter++;                    
                }
            }

        }

        private string AddId(string id)
        {
            var lastLoadedId = id;
            var lastLoadedIdNumber = int.Parse(lastLoadedId.Replace(ImdbApi.MoiveIdCode, ""));
            lastLoadedIdNumber += 1;
            //- lastLoadedIdNumber.ToString().Length
            var newMovieNumber = lastLoadedIdNumber.ToString().PadLeft(ImdbApi.MovieIdStartNumber.Length, '0');
            return ImdbApi.MoiveIdCode + newMovieNumber;
        }

        private bool AddNewMovie(string newMovieId)
        {
            return _imdbService.GetMovieByImdbId(newMovieId);
        }
    }
}