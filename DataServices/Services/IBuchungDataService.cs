using DataServices.Services.Base;
using System.Collections.Generic;

namespace DataServices.Services
{
    public interface IBuchungDataService : IDataService<BusinessModels.Buchung, Entities.Buchung>
    {
        List<BusinessModels.Buchung> GetByMonth(int year, int month);
    }
}
