using System;

namespace Jobby.Domain.Models
{
    public enum JobType
    {
        Recurrent,
        RunOnce
    }

    public class Job
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Cron { get; set; }
        public DateTimeOffset RunDateTime { get; set; }
        public JobType Type { get; set; }
        public bool IsRun { get; set; } = false;
    }
}
