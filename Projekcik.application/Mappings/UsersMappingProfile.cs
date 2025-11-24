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
            // Stare mapowanie (do rejestracji/wyświetlania)
            CreateMap<Projekcik.Entities.Users, Projekcik.application.Users.UsersDto>()
                .ReverseMap();

            // NOWE mapowanie (do edycji)
            CreateMap<Projekcik.Entities.Users, Projekcik.application.Users.UserEditDto>()
                .ReverseMap();
            // ReverseMap pozwoli nam zamienić Encję -> EditDto przy wejściu na formularz
        }
    }
}
