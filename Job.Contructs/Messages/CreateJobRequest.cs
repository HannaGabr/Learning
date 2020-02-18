using Jobby.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Jobby.Contracts.Messages
{
    public class CreateJobRequest
    {
        public string Description { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Cron { get; set; }
        public DateTimeOffset RunDateTime { get; set; }
        public JobType Type { get; set; }
    }
}
