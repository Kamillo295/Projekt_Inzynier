using AutoMapper;
using Projekcik.Entities;
using Projekcik.application.Robots;

public class RobotMappingProfile : Profile
{
    public RobotMappingProfile()
    {
        CreateMap<Robots, RobotsDto>()
            // Mapowanie zwykłych pól dzieje się automatycznie (jeśli nazwy są te same)

            // Mapowanie Nazwy Drużyny z obiektu powiązanego
            .ForMember(dest => dest.NazwaDruzyny, opt => opt.MapFrom(src => src.Team.NazwaDruzyny)) // Zmień .Nazwa na właściwe pole w klasie Team

            // Mapowanie Nazwy Kategorii z obiektu powiązanego
            .ForMember(dest => dest.NazwaKategorii, opt => opt.MapFrom(src => src.Categories.NazwaKategorii)); // Zmień .Nazwa na właściwe pole w klasie Categories

        // Mapowanie w drugą stronę (z formularza do bazy)
        CreateMap<RobotsDto, Robots>();
    }
}