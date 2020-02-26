using Jobby.Services.Interfaces;
using Jobby.Domain.Models;
using Jobby.Contracts.Messages;
using Jobby.Contracts.Models;
using Jobby.Services.JobTracking.Models;
using Jobby.Services.Scheduler.Interfaces;
using Jobby.Services.Scheduler.Models;
using AutoMapper;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService jobService;
        private readonly IJobTrackingService jobTrackingService;
        private readonly IMapper mapper;
        private readonly ICronHelper cronHelper;
 
        public JobController(IJobService jobService, IJobTrackingService jobTrackingService, IMapper mapper, ICronHelper cronHelper)
        {
            this.jobService = jobService;
            this.jobTrackingService = jobTrackingService;
            this.mapper = mapper;
            this.cronHelper = cronHelper;
        }

        [HttpGet]
        [Route("GetJobsWithLastInstance")]
        public async Task<ActionResult<GetJobsWithLastInstanceResponse>> GetJobsWithLastInstanceAsync()
        {
            Expression<Func<Job, JobInstance, JobWithInstance>> transformToViewModel = (job, jobInstance) =>
                new JobWithInstance
                {
                    Id = job.Id,
                    Description = job.Description,
                    Cron = job.Cron,
                    Email = job.Email,
                    RunDateTime = job.RunDateTime,
                    Type = job.Type,
                    LastRunStatus = jobInstance.Status
                };

            var jobsWithInstance = await jobTrackingService.GetJobsWithLastInstanceAsync(transformToViewModel);
            var result = new GetJobsWithLastInstanceResponse { JobsWithInstance = jobsWithInstance };

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetJobByIdResponse>> GetByIdAsync(string id)
        {
            var job = await jobService.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            var response = mapper.Map<GetJobByIdResponse>(job);

            return response;
        }

        [HttpPost]
        [Route("CreateRecurringJob")]
        public async Task<ActionResult<CreateJobResponse>> CreateRecurringJobAsync(
            [FromBody] CreateRecurringJobRequest createJobRequest)
        {
            var job = mapper.Map<CreateRecurringJobAppModel>(createJobRequest);
            var schedule = mapper.Map<Schedule>(createJobRequest);
            var cron = cronHelper.GetCron(schedule);
            job.Cron = cron;
            string jobId = await jobService.CreateRecurringJobAsync(job);

            var response = new CreateJobResponse() { JobId = jobId };

            return Created($"/job/{jobId}", response);
        }

        [HttpPost]
        [Route("CreateRunOnceJob")]
        public async Task<ActionResult<CreateJobResponse>> CreateRunOnceJobAsync(
            [FromBody] CreateRunOnceJobRequest createJobRequest)
        {
            var job = mapper.Map<CreateRunOnceJobAppModel>(createJobRequest);
            string jobId = await jobService.CreateRunOnceJobAsync(job);

            var response = new CreateJobResponse() { JobId = jobId };

            return Created($"/job/{jobId}", response);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            Response.Headers.Add("Allow", "GET, POST, DELETE");

            return new StatusCodeResult(405);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await jobService.RemoveJobAsync(id);

            return NoContent();
        }
    }
}
