using AutoMapper;

namespace DataServices.MappingProfiles
{
    public class KonfigurationMappingProfile : Profile
    {
        public KonfigurationMappingProfile()
        {
            CreateMap<Entities.Konfiguration, BusinessModels.Konfiguration>()
                .ReverseMap();
        }
    }
}
