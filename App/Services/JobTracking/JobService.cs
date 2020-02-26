using Jobby.Domain.Models;
using Jobby.Repository.Interfaces;
using Jobby.Services.Interfaces;
using Jobby.Services.JobTracking.Models;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace Jobby.Services
{
    // wrap with exeptions and log to understand exp lvl
    public class JobService : IJobService
    {
        private readonly IJobRepository jobRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IScheduler scheduler;
        private readonly IMapper mapper;

        public JobService(
            IJobRepository jobRepository,
            IUnitOfWork unitOfWork,
            IScheduler scheduler,
            IMapper mapper)
        {
            this.jobRepository = jobRepository;
            this.unitOfWork = unitOfWork;
            this.scheduler = scheduler;
            this.mapper = mapper;
        }

        public async Task<string> CreateRunOnceJobAsync(CreateRunOnceJobAppModel createJobModel)
        {
            //TODO param validation, accepts dateTime in future

            var job = mapper.Map<Job>(createJobModel);
            string id = jobRepository.NextIdentity();
            job.Id = id;
            job.Type = JobType.RunOnce;

            jobRepository.Add(job);
            await unitOfWork.CompleteAsync();

            scheduler.RunOnce<IJobTrackingService>((service) => service.Run(job.Id), job.RunDateTime.Value);

            return id;
        }

        public async Task<string> CreateRecurringJobAsync(CreateRecurringJobAppModel createJobModel)
        {
            //TODO param validation, accepts valid crone

            var job = mapper.Map<Job>(createJobModel);
            string id = jobRepository.NextIdentity();
            job.Id = id;
            job.Type = JobType.Recurring;

            jobRepository.Add(job);
            await unitOfWork.CompleteAsync();

            scheduler.ScheduleOrUpdate<IJobTrackingService>(
                job.Id,
                (service) => service.Run(job.Id),
                job.Cron);

            return id;
        }

        public async Task<Job> GetJobByIdAsync(string id)
        {
            return await jobRepository.GetByIdAsync(id);
        }

        public async Task UpdateJobAsync(Job job)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveJobAsync(string jobId)
        {
            var job = await jobRepository.GetByIdAsync(jobId);
            await jobRepository.RemoveAsync(jobId);
            await unitOfWork.CompleteAsync();

            if (job.Type == JobType.Recurring)
            {
                scheduler.RemoveScheduleIfExists(jobId);
            }
        }
    }
}
