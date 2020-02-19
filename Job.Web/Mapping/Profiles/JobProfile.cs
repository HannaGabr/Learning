using Jobby.Contracts.Messages;
using Jobby.Domain.Models;
using AutoMapper;
using Jobby.Services.JobTracking.Models;

namespace Jobby.Mapping.Profiles
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<CreateRunOnceJobRequest, CreateRunOnceJobAppModel>();
            CreateMap<CreateRecurringJobRequest, CreateRecurringJobAppModel>();
            CreateMap<Job, GetJobByIdResponse>();
        }
    }
}
