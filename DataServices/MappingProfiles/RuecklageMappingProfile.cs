using AutoMapper;

namespace DataServices.MappingProfiles
{
    public class RuecklageMappingProfile : Profile
    {
        public RuecklageMappingProfile()
        {
            CreateMap<Entities.Ruecklage, BusinessModels.Ruecklage>()
                .ReverseMap();
        }
    }
}
