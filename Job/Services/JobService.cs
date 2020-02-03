using Jobby.Domain.Models;
using Jobby.Repository.Interfaces;
using Jobby.Services.Interfaces;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobby
{
    // wrap with exeptions and log to understand exp lvl
    public class JobService : IJobService
    {
        private readonly IJobRepository jobRepository;
        private readonly IUnitOfWork unitOfWork;

        public JobService(IJobRepository jobRepository, IUnitOfWork unitOfWork)
        {
            this.jobRepository = jobRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> CreateJobAsync(Job job)
        {
            //TODO param validation, accepts crone

            string id = jobRepository.NextIdentity();
            job.Id = id;
            jobRepository.Add(job);

            await unitOfWork.CompleteAsync();

            //TODO run in scheduler
            
            return id;
        }

        public async Task<IEnumerable<T>> GetJobsWithInstanceAsync<T>(Expression<Func<Job, JobInstance, T>> transform)
        {
            var jobsWithInstances = await unitOfWork.GetJobsWithLastInstance(transform);

            return jobsWithInstances;
        }

        public async Task UpdateJobAsync(Job job)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveJobAsync(string jobId)
        {
            await jobRepository.RemoveAsync(jobId);

            //TODO remove from scheduler

            await unitOfWork.CompleteAsync();
        }
    }
}
