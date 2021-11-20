using AutoMapper;

namespace DataServices.MappingProfiles
{
    public class GutscheinMappingProfile : Profile
    {
        public GutscheinMappingProfile()
        {
            CreateMap<Entities.Gutschein, BusinessModels.Gutschein>()
                .ReverseMap();
        }
    }
}
