using Jobby.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Jobby.Contracts.Messages
{
    public class CreateRunOnceJobRequest
    {
        public string Description { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTimeOffset? RunDateTime { get; set; }
    }
}
