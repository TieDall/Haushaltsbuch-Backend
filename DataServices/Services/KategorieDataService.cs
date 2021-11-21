using AutoMapper;
using DataServices.DbContexte;
using DataServices.Services.Base;

namespace DataServices.Services
{
    public class KategorieDataService :  DataService<BusinessModels.Kategorie, Entities.Kategorie>, IKategorieDataService
    {
        private readonly IMapper _mapper;

        public KategorieDataService(
            HaushaltsbuchContext haushaltsbuchContext,
            IMapper mapper) : base(haushaltsbuchContext, mapper)
        {
            _mapper = mapper;
        }
    }
}
