using AutoMapper;
using System;

namespace DataServices.MappingProfiles
{
    public class DauerauftragMappingProfile : Profile
    {
        public DauerauftragMappingProfile()
        {
            // TODO 
            CreateMap<Entities.Dauerauftrag, BusinessModels.Dauerauftrag>()
                //.ForMember(
                //    model => model.IsAktiv, 
                //    opt => opt.MapFrom(
                //        entity => (entity.Beginn <= DateTime.Now) && (entity.Ende == null || DateTime.Now <= entity.Ende)))
                .ReverseMap();
        }
    }
}
