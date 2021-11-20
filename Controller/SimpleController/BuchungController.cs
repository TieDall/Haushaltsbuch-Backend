using BusinessModels;
using DataServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controller.SimpleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuchungController : ControllerBase
    {
        private readonly IBuchungDataService _buchungDataService;

        public BuchungController(
            IBuchungDataService buchungDataService) 
        {
            _buchungDataService = buchungDataService;
        }

        [HttpPost]
        public async Task<ActionResult<Buchung>> AddBuchung(Buchung buchung) 
        {
            var result = await _buchungDataService.Add(buchung);
            return CreatedAtAction(nameof(GetBuchung), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Buchung>> GetBuchung(long id) 
        {
            return Ok(await _buchungDataService.Get(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Buchung>> UpdateBuchung(long id, Buchung buchung) 
        {
            return Ok(await _buchungDataService.Update(buchung));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuchung(long id) 
        {
            await _buchungDataService.Delete(id);
            return Ok();
        }

        [HttpGet("GetByMonth/{year}/{month}")]
        public async Task<ActionResult<IEnumerable<Buchung>>> GetBuchungenByMonth(int year, int month)
        {
            return Ok(_buchungDataService.GetByMonth(year, month));
        }
    }
}
