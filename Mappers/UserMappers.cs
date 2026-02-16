using AutoMapper;
using BEPersonal.DTOs.In;
using BEPersonal.DTOs.Out;
using BEPersonal.Entities;

namespace BEPersonal.Mappers
{
    public class UserMappers : Profile
    {
        public UserMappers()
        {
            CreateMap<UserDTOIn, User>();
            CreateMap<User, UserDTOOut>();
        }       
    }
}
