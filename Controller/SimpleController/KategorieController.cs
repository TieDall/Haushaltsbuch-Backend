using BusinessModels;
using DataServices.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controller.SimpleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategorieController : ControllerBase
    {
        private readonly IKategorieDataService _kategorieDataService;

        public KategorieController(
            IKategorieDataService kategorieDataService)
        {
            _kategorieDataService = kategorieDataService;
        }

        [HttpPost]
        public async Task<ActionResult<Kategorie>> AddKategorie(Kategorie kategorie)
        {
            var result = await _kategorieDataService.Add(kategorie);
            return CreatedAtAction(nameof(GetKategorie), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kategorie>>> GetKategorien()
        {
            return Ok(await _kategorieDataService.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kategorie>> GetKategorie(long id)
        {
            return Ok(await _kategorieDataService.Get(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Kategorie>> UpdateKategorie(long id, Kategorie kategorie)
        {
            return Ok(await _kategorieDataService.Update(kategorie));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuchung(long id)
        {
            await _kategorieDataService.Delete(id);
            return Ok();
        }
    }
}
