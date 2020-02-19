using System;

namespace Jobby.Domain.Models
{
    public enum RunStatus
    {
        Pending,
        Success,
        Error
    }

    public class JobInstance
    {
        public string Id { get; set; }
        public string JobId { get; set; }
        public DateTime StartedAt { get; set; }
        public string Error { get; set; }
        public RunStatus Status { get; set; }

        public static JobInstance Create(string id, Job job)
        {
            var jobInstance = new JobInstance()
            {
                Id = id,
                JobId = job.Id,
                StartedAt = DateTime.UtcNow,
                Error = null,
                Status = RunStatus.Pending
            };

            return jobInstance;
        }
    }
}
