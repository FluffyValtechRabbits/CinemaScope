using Quartz;
using Quartz.Spi;
using System;

namespace CinemaScopeWeb.ScheduledTasks
{

    public class DemoJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DemoJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_serviceProvider.GetService(typeof(TaskService));
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}