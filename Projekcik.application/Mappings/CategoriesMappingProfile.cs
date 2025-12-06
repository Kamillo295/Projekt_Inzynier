using Projekcik.Entities;
using Projekcik.application.Categories;
using AutoMapper;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Categories, CategoryDto>()
            // 1. Mapowanie licznika (Count)
            .ForMember(dest => dest.LiczbaRobotow, opt => opt.MapFrom(src => src.Roboty.Count))

            // 2. Mapowanie nazw robotów (Select)
            .ForMember(dest => dest.NazwyRobotow, opt => opt.MapFrom(src => src.Roboty.Select(r => r.NazwaRobota).ToList()));

        // Mapowanie odwrotne (z formularza do bazy)
        CreateMap<CategoryDto, Categories>()
            .ForMember(dest => dest.Roboty, opt => opt.Ignore()); // Ignorujemy listę przy tworzeniu kategorii
    }
}