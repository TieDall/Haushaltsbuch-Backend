using DataServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controller.SimpleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GutscheinController : CrudController<BusinessModels.Gutschein, DataServices.Entities.Gutschein>
    {
        public GutscheinController(
            IGutscheinDataService dataService) : base(dataService)
        {
        }
    }
}
