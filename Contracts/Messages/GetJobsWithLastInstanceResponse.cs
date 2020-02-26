using Jobby.Contracts.Models;
using System.Collections.Generic;

namespace Jobby.Contracts.Messages
{
    public class GetJobsWithLastInstanceResponse
    {
        public IEnumerable<JobWithInstance> JobsWithInstance { get; set; }
    }
}
