using AutoMapper;
using Projekcik.Entities;
using Projekcik.application.Robots;

public class RobotMappingProfile : Profile
{
    public RobotMappingProfile()
    {
        CreateMap<Robots, RobotsDto>()
            .ForMember(d => d.NazwaKategorii, o => o.MapFrom(s => s.Categories.NazwaKategorii))
            .ForMember(d => d.NazwaDruzyny, o => o.MapFrom(s => s.Team.NazwaDruzyny))

            .ForMember(d => d.NazwaOperatora, o => o.MapFrom(s =>
                s.Zawodnik != null ? $"{s.Zawodnik.Imie} {s.Zawodnik.Nazwisko}" : "Brak operatora"));

        CreateMap<RobotsDto, Robots>();
    }
}