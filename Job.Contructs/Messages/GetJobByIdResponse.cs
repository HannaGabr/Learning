using Jobby.Domain.Models;
using System;

namespace Jobby.Contracts.Messages
{
    public class GetJobByIdResponse
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Cron { get; set; }
        public DateTimeOffset? RunDateTime { get; set; }
        public JobType Type { get; set; }
        public bool IsRun { get; set; }
    }
}
