using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Projekcik.application.Users;
using Projekcik.Entities;

namespace Projekcik.application.Mappings
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<UsersDto, Projekcik.Entities.Users>()
                .ForMember(e => e.Imie, opt => opt.MapFrom(src => src.Imie)); //samo się robi przez auto mapper dla reszty (to tylko przykład)
        }
    }
}
