using DataServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controller.SimpleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategorieController : CrudController<BusinessModels.Kategorie, DataServices.Entities.Kategorie>
    {
        private readonly IKategorieDataService _kategorieDataService;

        public KategorieController(
            IKategorieDataService kategorieDataService) : base(kategorieDataService)
        {
            _kategorieDataService = kategorieDataService;
        }
    }
}
