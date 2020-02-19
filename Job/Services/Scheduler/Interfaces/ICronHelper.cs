using Jobby.Services.Scheduler.Models;
using System;

namespace Jobby.Services.Scheduler.Interfaces
{
    public interface ICronHelper
    {
        DateTimeOffset GetNextOccurrenceFromNow(string cron);
        string GetCron(Schedule schedule);
    }
}
