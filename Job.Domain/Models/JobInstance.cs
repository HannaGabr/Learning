using System;

namespace Job.Domain.Models
{
    enum ExecutionStatus
    {
        Pending,
        Success,
        Error
    }

    class JobInstance
    {
        public string Id { get; set; }
        public string JobId { get; set; }
        public DateTime StartedAt { get; set; }
        public string Error { get; set; }
        public ExecutionStatus Status { get; set; }
    }
}
