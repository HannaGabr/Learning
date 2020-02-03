namespace Jobby.Domain.Models
{
    public class Job
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Cron { get; set; }
        public bool IsRunning { get; set; }
    }
}
