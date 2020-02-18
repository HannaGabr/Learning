using Jobby.Domain.Models;
using System.Threading.Tasks;

namespace Jobby.Services.Interfaces
{
    public interface IJobService
    {
        Task<Job> GetJobByIdAsync(string id);
        Task<string> CreateRunOnceJobAsync(Job job);
        Task<string> CreateRecurrentJobAsync(Job job);
        Task UpdateJobAsync(Job job);
        Task RemoveJobAsync(string jobId);
    }
}
