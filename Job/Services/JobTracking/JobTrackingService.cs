using Jobby.Domain.Models;
using Jobby.Repository.Interfaces;
using Jobby.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jobby.Services
{
    public class JobTrackingService : IJobTrackingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJobRepository jobRepository;
        private readonly IJobInstanceRepository jobInstanceRepository;
        private readonly IScheduler scheduler;

        public JobTrackingService(
            IJobRepository jobRepository,
            IJobInstanceRepository jobInstanceRepository,
            IUnitOfWork unitOfWork,
            IScheduler scheduler)
        {
            this.jobRepository = jobRepository;
            this.jobInstanceRepository = jobInstanceRepository;
            this.unitOfWork = unitOfWork;
            this.scheduler = scheduler;
        }

        public async Task<IEnumerable<T>> GetJobsWithLastInstanceAsync<T>(Expression<Func<Job, JobInstance, T>> transform)
        {
            var jobsWithInstances = await unitOfWork.GetJobsWithLastInstance(transform);

            return jobsWithInstances;
        }

        public async void Run(string jobId)
        {
            try
            {
                var job = await jobRepository.GetByIdAsync(jobId);
                if (job == null)
                {
                    if (job.Type == JobType.Recurring)
                    {
                        scheduler.RemoveScheduleIfExists(jobId);
                    }

                    return;
                }

                job.IsRun = true;
                jobRepository.Update(job);

                var jobInstanceId = jobInstanceRepository.NextIdentity();
                var jobInstance = JobInstance.Create(jobInstanceId, job);
                jobInstanceRepository.Add(jobInstance);

                await unitOfWork.CompleteAsync();

                //TODO Send
                if (false)
                {
                    jobInstance.Status = RunStatus.Error;
                    jobInstance.Error = "error";
                } else
                {
                    jobInstance.Status = RunStatus.Success;
                }
                jobInstanceRepository.Update(jobInstance);

                job.IsRun = false;
                jobRepository.Update(job);

                await unitOfWork.CompleteAsync();
            }
            catch (Exception exp)
            {
            }
        }
    }
}
