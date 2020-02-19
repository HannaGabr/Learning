using System;

namespace Jobby.Services.JobTracking.Models
{
    public class CreateRunOnceJobAppModel
    {
        public string Description { get; set; }
        public string Email { get; set; }
        public DateTimeOffset RunDateTime { get; set; }
    }
}
