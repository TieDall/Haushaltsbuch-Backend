using AutoMapper;

namespace DataServices.MappingProfiles
{
    public class KategorieMappingProfile : Profile
    {
        public KategorieMappingProfile()
        {
            CreateMap<Entities.Kategorie, BusinessModels.Kategorie>()
                .ReverseMap();
        }
    }
}
