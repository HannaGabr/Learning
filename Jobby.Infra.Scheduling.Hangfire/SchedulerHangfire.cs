﻿using Jobby.Services.Interfaces;
using Hangfire;
using System;
using System.Linq.Expressions;

namespace Jobby.Infra.Scheduling.HangF
{
    public class SchedulerHangfire : IScheduler
    {
        public void ScheduleOrUpdate<T>(string id, Expression<Action<T>> e, string cron)
        {
            RecurringJob.AddOrUpdate(id, e, cron);
        }

        public void RunOnce<T>(Expression<Action<T>> e, TimeSpan delay)
        {
            BackgroundJob.Schedule(e, delay);
        }

        public void RemoveScheduleIfExists(string id)
        {
            RecurringJob.RemoveIfExists(id);
        }
    }
}
