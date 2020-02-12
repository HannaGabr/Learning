using Jobby.Services.Interfaces;
using Jobby.Domain.Models;
using Jobby.Contracts.Messages;
using Jobby.Contracts.Models;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Jobby.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService jobService;
        private readonly IJobTrackingService jobTrackingService;
        private readonly IMapper mapper;
 
        public JobController(IJobService jobService, IJobTrackingService jobTrackingService, IMapper mapper)
        {
            this.jobService = jobService;
            this.jobTrackingService = jobTrackingService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetJobsWithInstance")]
        public async Task<ActionResult<GetJobsWithInstanceResponse>> GetJobsWithInstanceAsync()
        {
            Expression<Func<Job, JobInstance, JobWithInstance>> transformToViewModel = (job, jobInstance) =>
                new JobWithInstance
                {
                    Id = job.Id,
                    Description = job.Description,
                    Cron = job.Cron,
                    Email = job.Email,
                    LastRunStatus = jobInstance.Status.ToString()
                };

            var jobsWithInstance = await jobTrackingService.GetJobsWithLastInstanceAsync(transformToViewModel);
            var result = new GetJobsWithInstanceResponse { JobsWithInstance = jobsWithInstance };

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
        public async Task<ActionResult<CreateJobResponse>> PostAsync([FromBody] CreateJobRequest createJobRequest)
        {
            var job = mapper.Map<Job>(createJobRequest);
            string jobId = await jobService.CreateJobAsync(job);

            var response = new CreateJobResponse() { JobId = jobId };

            return Created($"/job/{jobId}", response);
        }

        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await jobService.RemoveJobAsync(id);

            return NoContent();
        }
    }
}
