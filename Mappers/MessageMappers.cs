using AutoMapper;
using BEPersonal.DTOs.In;
using BEPersonal.DTOs.Out;
using BEPersonal.Entities;

namespace BEPersonal.Mappers
{
    public class MessageMappers : Profile
    {
        public MessageMappers()
        {
            CreateMap<MessageDTOIn, Message>();

            CreateMap<Message, MessageDTOOut>()
                .ForMember(dest => dest.UserId,
                           opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Email,
                           opt => opt.MapFrom(src => src.User.Email));
        }

    }
}
