using AutoMapper;
using Projekcik.Entities;
using Projekcik.application.Users; 

namespace Projekcik.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, UsersDto>().ReverseMap();

            // W obie strony
            CreateMap<Users, UserEditDto>().ReverseMap();
        }
    }
}