using AutoMapper;
using DataServices.DbContexte;
using DataServices.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataServices.Services
{
    public class GutscheinDataService : DataService<BusinessModels.Gutschein, DataServices.Entities.Gutschein>, IGutscheinDataService
    {
        public GutscheinDataService(
            HaushaltsbuchContext haushaltsbuchContext, 
            IMapper mapper) : base(haushaltsbuchContext, mapper)
        {
        }
    }
}
