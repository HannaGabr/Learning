using Jobby.Contracts.Messages;
using Jobby.Domain.Models;
using Jobby.Services.JobTracking.Models;
using Jobby.Services.Scheduler.Models;
using AutoMapper;

namespace Jobby.Mapping.Profiles
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<CreateRunOnceJobRequest, CreateRunOnceJobAppModel>();
            CreateMap<CreateRecurringJobRequest, CreateRecurringJobAppModel>();
            CreateMap<CreateRecurringJobRequest, Schedule>();
            CreateMap<Job, GetJobByIdResponse>();
        }
    }
}
