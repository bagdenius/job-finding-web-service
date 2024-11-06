using AutoMapper;
using Entities;
using Models;

namespace Mappers
{
    public class ResumeMapper : Profile
    {
        public ResumeMapper()
        {
            CreateMap<Resume, ResumeModel>().ReverseMap();
        }
    }
}
