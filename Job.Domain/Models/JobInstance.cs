using System;

namespace Jobby.Domain.Models
{
    public enum ExecutionStatus
    {
        Pending,
        Success,
        Error,
        Warning
    }

    public class JobInstance
    {
        public string Id { get; set; }
        public string JobId { get; set; }
        public DateTime StartedAt { get; set; }
        public string Error { get; set; }
        public ExecutionStatus Status { get; set; }

        public static JobInstance Create(string id, Job job)
        {
            var jobInstance = new JobInstance()
            {
                Id = id,
                JobId = job.Id,
                StartedAt = DateTime.UtcNow,
                Error = null,
                Status = ExecutionStatus.Pending
            };

            return jobInstance;
        }
    }
}
