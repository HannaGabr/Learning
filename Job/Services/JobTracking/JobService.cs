using Jobby.Domain.Models;
using Jobby.Repository.Interfaces;
using Jobby.Services.Interfaces;
using System.Threading.Tasks;

namespace Jobby.Services
{
    // wrap with exeptions and log to understand exp lvl
    public class JobService : IJobService
    {
        private readonly IJobRepository jobRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IScheduler scheduler;

        public JobService(
            IJobRepository jobRepository,
            IUnitOfWork unitOfWork,
            IScheduler scheduler)
        {
            this.jobRepository = jobRepository;
            this.unitOfWork = unitOfWork;
            this.scheduler = scheduler;
        }

        public async Task<string> CreateJobAsync(Job job)
        {
            //TODO param validation, accepts crone

            string id = jobRepository.NextIdentity();
            job.Id = id;
            jobRepository.Add(job);

            await unitOfWork.CompleteAsync();

            //TODO for once
            scheduler.ScheduleOrUpdate<IJobTrackingService>(
                id,
                (service) => service.Run(id),
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
            await jobRepository.RemoveAsync(jobId);
            await unitOfWork.CompleteAsync();

            scheduler.RemoveScheduleIfExists(jobId);
        }
    }
}
