using BusinessModels;
using DataServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Controller.SimpleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuchungController : CrudController<BusinessModels.Buchung, DataServices.Entities.Buchung>
    {
        private readonly IBuchungDataService _buchungDataService;

        public BuchungController(
            IBuchungDataService buchungDataService) : base(buchungDataService)
        {
            _buchungDataService = buchungDataService;
        }

        [HttpGet("GetByMonth/{year}/{month}")]
        public ActionResult<IEnumerable<Buchung>> GetBuchungenByMonth(int year, int month)
        {
            return Ok(_buchungDataService.GetByMonth(year, month));
        }
    }
}
