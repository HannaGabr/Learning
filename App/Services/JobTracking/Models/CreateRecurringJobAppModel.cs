namespace Jobby.Services.JobTracking.Models
{
    public class CreateRecurringJobAppModel
    {
        public string Description { get; set; }
        public string Email { get; set; }
        public string Cron { get; set; }
    }
}
