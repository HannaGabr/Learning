using Jobby.Services.Scheduler.Interfaces;
using Hangfire;
using NCrontab;
using System;
using Jobby.Services.Scheduler.Models;

namespace Jobby.Infra.CronTool
{
    public class NCronTabHangFHelper : ICronHelper
    {
        public string GetCron(Schedule schedule)
        {
            switch (schedule.Ocurrence)
            {
                case (Occurrence.Weekly):
                    {
                        return Cron.Weekly(schedule.DayOfWeek, schedule.Hour, schedule.Minute);
                    }
                case (Occurrence.Monthly):
                    {
                        return Cron.Monthly(schedule.Day, schedule.Hour, schedule.Minute);
                    }
                case (Occurrence.Yearly):
                    {
                        return Cron.Yearly((int)schedule.Month, schedule.Day, schedule.Hour, schedule.Minute);
                    }
                default:
                    throw new ArgumentException(message: "invalid enum value", paramName: nameof(Occurrence));
            };
        }

        public DateTimeOffset GetNextOccurrenceFromNow(string cron)
        {
            var schedule = CrontabSchedule.Parse(cron);
            var utcNow = DateTime.UtcNow;
            var nextOccurrenceUtc = schedule.GetNextOccurrence(utcNow);

            return nextOccurrenceUtc;
        }
    }
}
