using Jobby.Contracts.Messages;
using Jobby.Domain.Models;
using AutoMapper;
using Jobby.Services.JobTracking.Models;

namespace Jobby.Infra.Mapping.AutoMap.Profiles
{
    public class JobViewProfile : Profile
    {
        public JobViewProfile()
        {
            CreateMap<CreateRunOnceJobRequest, CreateRunOnceJobAppModel>();
            CreateMap<CreateRecurringJobRequest, CreateRecurringJobAppModel>();
            CreateMap<Job, GetJobByIdResponse>();
        }
    }
}
