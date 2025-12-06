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
            // Mapowanie operatora: Jeśli jest null, wpisz "Brak", w przeciwnym razie Imię + Nazwisko
            .ForMember(d => d.NazwaOperatora, o => o.MapFrom(s =>
                s.Zawodnik != null ? $"{s.Zawodnik.Imie} {s.Zawodnik.Nazwisko}" : "Brak operatora"));

        // Mapowanie w drugą stronę (z formularza do bazy)
        CreateMap<RobotsDto, Robots>();
    }
}