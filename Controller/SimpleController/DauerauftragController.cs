using DataServices.Services;
using DataServices.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controller.SimpleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DauerauftragController : CrudController<BusinessModels.Dauerauftrag, DataServices.Entities.Dauerauftrag>
    {
        private IDauerauftragDataService _dauerauftragDataService;
        public DauerauftragController(
            IDauerauftragDataService dataService) : base(dataService)
        {
            _dauerauftragDataService = dataService;
        }

        [HttpGet]
        public override async Task<ActionResult<BusinessModels.Dauerauftrag>> Get()
        {
            return await Task.FromResult(NotFound());
        }

        [HttpGet("GetGrouped")]
        public async Task<ActionResult<IEnumerable<BusinessModels.DauerauftragGrouped>>> GetGrouped() {
            return Ok(_dauerauftragDataService.GetGroupedByBezeichnungAndKategorie());
        }

        [HttpGet("GetByMonth/{year}/{month}")]
        public ActionResult<List<BusinessModels.Dauerauftrag>> GetByMonth(int year, int month)
        {
            return Ok(_dauerauftragDataService.GetByMonth(year, month));
        }

        [HttpGet("GetByBezeichnungAndKategorie/{bezeichnung}/{kategorieId}")]
        public async Task<ActionResult<IEnumerable<BusinessModels.Dauerauftrag>>> GetByBezeichnungAndKategorie(string bezeichnung, long kategorieId) 
        {
            return Ok(_dauerauftragDataService.GetByBezeichnungAndKategorie(bezeichnung, kategorieId));
        }

    }
}
