using System.Linq;
using AutoMapper;
using Projekcik.Entities;
using Projekcik.application.Teams;

public class TeamMappingProfile : Profile
{
    public TeamMappingProfile()
    {
        CreateMap<Team, TeamDto>()
            .ForMember(dest => dest.LiczbaZawodnikow, opt => opt.MapFrom(src => src.Zawodnicy.Count))
            .ForMember(dest => dest.NazwyRobotow, opt => opt.MapFrom(src => src.Roboty.Select(r => r.NazwaRobota).ToList()));

        CreateMap<TeamDto, Team>()
            .ForMember(dest => dest.Zawodnicy, opt => opt.Ignore()) // Ignorujemy przy zapisie, bo zawodników dodaje się inaczej
            .ForMember(dest => dest.Roboty, opt => opt.Ignore());
    }
}