using Jobby.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jobby.Services.Interfaces
{
    public interface IJobTrackingService
    {
        Task<IEnumerable<T>> GetJobsWithLastInstanceAsync<T>(Expression<Func<Job, JobInstance, T>> transform);

        void Run(string jobId);
    }
}
