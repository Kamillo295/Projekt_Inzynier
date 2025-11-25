using AutoMapper;
using Projekcik.Entities;
using Projekcik.application.Users; 

namespace Projekcik.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Twoje stare mapowanie (UsersDto)
            CreateMap<Users, UsersDto>().ReverseMap();

            // NOWE mapowanie (UserEditDto) - to naprawia Twój błąd!
            CreateMap<Users, UserEditDto>().ReverseMap();
        }
    }
}