using Jobby.Contracts.Models;
using System.Collections.Generic;

namespace Jobby.Contracts.Messages
{
    public class GetJobsWithInstanceResponse
    {
        public IEnumerable<JobWithInstance> JobsWithInstance { get; set; }
    }
}
