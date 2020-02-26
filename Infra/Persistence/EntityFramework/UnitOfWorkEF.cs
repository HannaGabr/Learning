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

        public UnitOfWorkEF(JobTrackingContext context)
        {
            dbContext = context;
        }

        public async Task<IEnumerable<T>> GetJobsWithLastInstance<T>(Expression<Func<Job, JobInstance, T>> transform)
        {
            var lastJobInstances = dbContext.JobInstances
                .GroupBy(
                    jobInstance => jobInstance.JobId,
                    (key, group) => group.First(j => j.StartedAt == group.Max(i => i.StartedAt))
                );
            var jobsWithLastInstance = await dbContext.Jobs
                .GroupJoin(
                    lastJobInstances,
                    Job => Job.Id,
                    JobInstance => JobInstance.JobId,
                    (job, jobInstances) => new { Job = job, JobInstances = jobInstances })
                .SelectMany(
                    xy => xy.JobInstances.DefaultIfEmpty(new JobInstance()),
                    (xy, jobInstance) => transform.Compile()(xy.Job, jobInstance)
                )
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
