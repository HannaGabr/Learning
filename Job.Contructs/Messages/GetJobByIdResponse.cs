using System;
using System.Collections.Generic;
using System.Text;

namespace Jobby.Contracts.Messages
{
    public class GetJobByIdResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Cron { get; set; }
        public bool IsRun { get; set; }
    }
}
