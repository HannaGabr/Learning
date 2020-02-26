using Jobby.Domain.Models;
using Jobby.Services.JobTracking.Models;
using System.Threading.Tasks;

namespace Jobby.Services.Interfaces
{
    public interface IJobService
    {
        Task<Job> GetJobByIdAsync(string id);
        Task<string> CreateRunOnceJobAsync(CreateRunOnceJobAppModel job);
        Task<string> CreateRecurringJobAsync(CreateRecurringJobAppModel job);
        Task UpdateJobAsync(Job job);
        Task RemoveJobAsync(string jobId);
    }
}
