using MovieService.Interfaces;
using MovieService.UOW;
using Quartz;
using Quartz.Impl;
using System;
using System.Configuration;
using Ninject;
using Quartz.Ninject;
using MovieService.Interfaces.ServiceInterfaces;
using MovieService.Services;

namespace CinemaScopeWeb.ScheduledTasks
{
    public class SchedulerService
    {
        private static readonly string ScheduleCronExpression = ConfigurationManager.AppSettings["SchedularService"];

        public static async System.Threading.Tasks.Task StartAsync()
        {
            var kernel = new StandardKernel();

            // setup Quartz scheduler that uses our NinjectJobFactory
            kernel.Bind<IScheduler>().ToMethod(x =>
            {
                var sched = new StdSchedulerFactory().GetScheduler();
                sched.Result.JobFactory = new NinjectJobFactory(kernel);
                return sched.Result;
            });

            // add your Ninject bindings as you normally would (these are the bindings that your jobs require)
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IImdbService>().To<ImdbService>();

            var scheduler = kernel.Get<IScheduler>();

            //try
            //{
                //var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                if (!scheduler.IsStarted)
                {   
                    //scheduler.JobFactory = jobFactory;
                    await scheduler.Start();
                }
                var job = JobBuilder.Create<TaskService>().Build();
                var trigger = TriggerBuilder.Create()
                  .WithIdentity("trigger1", "group1")
                  .StartNow()
                  .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(5)
                    .RepeatForever())
                  .Build();
                await scheduler.ScheduleJob(job, trigger);
            //}
            //catch (Exception ex)
            //{
            //}
        }
    }
}