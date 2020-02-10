using Jobby.Contracts.Messages;
using Jobby.Contracts.Models;
using Jobby.Domain.Models;
using AutoMapper;

namespace Jobby.Infra.Mapping.AutoMap.Profiles
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<CreateJobRequest, Job>();
            CreateMap<Job, GetJobByIdResponse>();
        }
    }
}
