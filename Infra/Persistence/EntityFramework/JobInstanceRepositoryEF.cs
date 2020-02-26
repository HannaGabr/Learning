using System;
using System.Threading.Tasks;
using Jobby.Domain.Models;
using Jobby.Repository.Interfaces;

namespace Jobby.Infra.Persistence.EF
{
    public class JobInstanceRepositoryEF : RepositoryEF<JobInstance>, IJobInstanceRepository
    {
        public JobInstanceRepositoryEF(JobTrackingContext context) : base(context)
        {
        }
    }
}
