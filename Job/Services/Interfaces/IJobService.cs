using Jobby.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jobby.Services.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<T>> GetJobsWithLastInstanceAsync<T>(Expression<Func<Job, JobInstance, T>> transform);
        Task<Job> GetJobByIdAsync(string id);
        Task<string> CreateJobAsync(Job job);
        Task UpdateJobAsync(Job job);
        Task RemoveJobAsync(string jobId);
    }
}
