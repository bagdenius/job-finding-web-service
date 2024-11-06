using AutoMapper;
using Entities;
using Models;

namespace Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserModel>().ReverseMap();
        }
    }
}
