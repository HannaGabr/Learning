using Jobby.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jobby.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IJobRepository JobRepository { get; }
        IJobInstanceRepository JobInstanceRepository { get; }

        Task<IEnumerable<T>> GetJobsWithLastInstance<T>(Expression<Func<Job, JobInstance, T>> transform);
        Task<int> CompleteAsync();
    }
}
