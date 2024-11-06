using AutoMapper;
using Entities;
using Models;

namespace Mappers
{
    public class VacancyMapper : Profile
    {
        public VacancyMapper()
        {
            CreateMap<Vacancy, VacancyModel>().ReverseMap();
        }
    }
}
