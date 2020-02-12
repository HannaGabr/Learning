using System;
using System.Linq.Expressions;

namespace Jobby.Services.Interfaces
{
    public interface IScheduler
    {
        void ScheduleOrUpdate<T>(string id, Expression<Action<T>> e, string cron);
        void RunOnce<T>(Expression<Action<T>> e, TimeSpan delay);
        void RemoveScheduleIfExists(string id);
    }
}
