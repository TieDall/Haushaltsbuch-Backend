using DataServices.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace Controller.SimpleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GutscheinController : CrudController<BusinessModels.Gutschein, DataServices.Entities.Gutschein>
    {
        public GutscheinController(
            IDataService<BusinessModels.Gutschein, DataServices.Entities.Gutschein> dataService) : base(dataService)
        {
        }
    }
}
