using DataServices.Services.Base;
using DataServices.DbContexte;
using AutoMapper;
using System.Collections.Generic;
using BusinessModels;
using System.Linq;

namespace DataServices.Services
{
    public class BuchungDataService : DataService<BusinessModels.Buchung, Entities.Buchung>, IBuchungDataService
    {
        private readonly IMapper _mapper;

        public BuchungDataService(
            HaushaltsbuchContext haushaltsbuchContext,
            IMapper mapper): base(haushaltsbuchContext, mapper) 
        {
            _mapper = mapper;
        }

        public List<Buchung> GetByMonth(int year, int month)
        {
            var queryresult = GetDefaultQuery()
                .ToList()
                .Where(x => x.Buchungstag.Year == year && x.Buchungstag.Month == month)
                .ToList();

            var result = _mapper.Map<List<Buchung>>(queryresult);

            return result;
        }
    }
}
