using System.Linq; // Ważne dla .Select()
using AutoMapper;
using Projekcik.Entities;
using Projekcik.application.Teams;

public class TeamMappingProfile : Profile
{
    public TeamMappingProfile()
    {
        CreateMap<Team, TeamDto>()
            // 1. Mapowanie Liczby Zawodników (Count)
            .ForMember(dest => dest.LiczbaZawodnikow, opt => opt.MapFrom(src => src.Zawodnicy.Count))

            // 2. Mapowanie Listy Nazw Robotów
            // Bierzemy kolekcję Roboty -> wybieramy tylko Nazwę -> zamieniamy na Listę stringów
            .ForMember(dest => dest.NazwyRobotow, opt => opt.MapFrom(src => src.Roboty.Select(r => r.NazwaRobota).ToList()));

        CreateMap<TeamDto, Team>()
            .ForMember(dest => dest.Zawodnicy, opt => opt.Ignore()) // Ignorujemy przy zapisie, bo zawodników dodaje się inaczej
            .ForMember(dest => dest.Roboty, opt => opt.Ignore());
    }
}