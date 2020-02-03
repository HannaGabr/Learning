using System;

namespace Jobby.Domain.Models
{
    public enum ExecutionStatus
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
        public ExecutionStatus Status { get; set; }
    }
}
