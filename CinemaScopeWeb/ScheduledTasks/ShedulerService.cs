using Quartz;
using Quartz.Impl;
using System;
using System.Configuration;

namespace CinemaScopeWeb.ScheduledTasks
{
    public class SchedulerService
    {
        private static readonly string ScheduleCronExpression = ConfigurationManager.AppSettings["SchedularService"];
        public static async System.Threading.Tasks.Task StartAsync()
        {
            try
            {
                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                if (!scheduler.IsStarted)
                {
                    await scheduler.Start();
                }
                var job = JobBuilder.Create<TaskService>().Build();
                var trigger = TriggerBuilder.Create()
                  .WithIdentity("trigger1", "group1")
                  .StartNow()
                  .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever())
                  .Build();
                await scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception ex)
            {
            }
        }
    }
}