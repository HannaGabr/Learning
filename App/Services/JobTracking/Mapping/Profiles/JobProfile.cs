using Jobby.Domain.Models;
using Jobby.Services.JobTracking.Models;
using AutoMapper;

namespace Jobby.Services.JobTracking.Mapping.Profiles
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<CreateRunOnceJobAppModel, Job>();
            CreateMap<CreateRecurringJobAppModel, Job>();
        }
    }
}
