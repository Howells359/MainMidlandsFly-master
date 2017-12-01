using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMidlandsFly
{
    public class AllocationScheduler
    {
        public static void Start()
        {
            try
            {
                ISchedulerFactory sf = new StdSchedulerFactory();
                IScheduler sc = sf.GetScheduler();


                IJobDetail job = JobBuilder.Create<AllocateGroundCrewJob>().Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                      (s =>
                         s.WithIntervalInHours(2)
                        //s.WithIntervalInSeconds(20)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                      )
                    .Build();

                // .RepeatForever())

                sc.ScheduleJob(job, trigger);
                sc.Start();

            }
            catch { }
        }

    }
}


