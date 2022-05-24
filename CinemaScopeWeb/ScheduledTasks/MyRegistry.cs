using FluentScheduler;
using MovieService.Interfaces;
using MovieService.Interfaces.ServiceInterfaces;

namespace CinemaScopeWeb.ScheduledTasks
{
    public class MyRegistry : Registry
    {
        public MyRegistry(IImdbService imdbService, IUnitOfWork unitOfWork)
        {
            Schedule(() => new SyncUpJob(imdbService, unitOfWork)).ToRunNow().AndEvery(1).Minutes();
        }
    }
}