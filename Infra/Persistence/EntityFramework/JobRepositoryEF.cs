using Jobby.Domain.Models;
using Jobby.Repository.Interfaces;

namespace Jobby.Infra.Persistence.EF
{
    public class JobRepositoryEF : RepositoryEF<Job>, IJobRepository
    {
        public JobRepositoryEF(JobTrackingContext context): base(context)
        {
        }
    }
}
