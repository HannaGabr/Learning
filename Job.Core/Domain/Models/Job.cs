using System;
using System.Collections.Generic;
using System.Text;

namespace JobScheduler.Domain.Models
{
    class Job
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Cron { get; set; }
        public bool IsRunning { get; set; }
    }
}
