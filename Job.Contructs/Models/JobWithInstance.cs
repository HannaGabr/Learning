using Jobby.Domain.Models;

namespace Jobby.Contracts.Models
{
    public class JobWithInstance
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Cron { get; set; }
        public string LastRunStatus { get; set; }
    }
}
