using AutoMapper;
using Resume_Portfolio.Models;

namespace Resume_Portfolio;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Job, JobViewModel>();
        CreateMap<JobViewModel, Job>();
        CreateMap<JobDto, Job>();
        CreateMap<Job, JobDto>();
    }
}