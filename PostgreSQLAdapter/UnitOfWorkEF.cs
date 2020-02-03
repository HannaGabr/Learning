using Jobby.Domain.Models;
using Jobby.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jobby.Infra.Persistence.EF
{
    public class UnitOfWorkEF : IUnitOfWork
    {
        private readonly JobTrackingContext dbContext;
        public IJobRepository JobRepository { get; }
        public IJobInstanceRepository JobInstanceRepository { get; }

        public UnitOfWorkEF(JobTrackingContext context)
        {
            dbContext = context;
            JobRepository = new JobRepositoryEF(context);
            JobInstanceRepository = new JobInstanceRepositoryEF(context);
        }

        public async Task<IEnumerable<T>> GetJobsWithLastInstance<T>(Expression<Func<Job, JobInstance, T>> transform)
        {
            var jobsWithLastInstance = await dbContext.Jobs
                .Join(dbContext.JobInstances, Job => Job.Id, JobInstance => JobInstance.JobId, transform)
                .ToListAsync();

            return jobsWithLastInstance;
        }

        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
