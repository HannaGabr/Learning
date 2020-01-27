using Job.Contracts.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Job.Contracts.Messages
{
    class UpdateJobRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Cron { get; set; }
    }

    class CreateJobRequest
    {
        public string Description { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Cron { get; set; }
    }

    class GetJobByIdResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Cron { get; set; }
        public bool IsRunning { get; set; }
    }

    class GetJobsResponse
    {
        public IEnumerable<GetJobModel> Jobs { get; set; }
    }
}
