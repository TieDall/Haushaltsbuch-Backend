using AutoMapper;

namespace DataServices.MappingProfiles
{
    public class BuchungMappingProfile : Profile
    {
        public BuchungMappingProfile()
        {
            CreateMap<Entities.Buchung, BusinessModels.Buchung>().ReverseMap();
        }
    }
}
