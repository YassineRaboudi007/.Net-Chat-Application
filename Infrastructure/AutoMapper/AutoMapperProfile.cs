using AutoMapper;
using ChatApplication.Application.Users.Commands.Register;
using ChatApplication.Domain.Entities;

namespace ChatApplication.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterUserDto, User>();
        }
    }
}
