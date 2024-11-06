using AutoMapper;
using Entities;
using Models;

namespace Mappers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Application, ApplicationModel>().ReverseMap();
            CreateMap<Vacancy, VacancyModel>().ReverseMap();
        }
    }
}
