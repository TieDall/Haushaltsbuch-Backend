using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuchungController : ControllerBase
    {
        private readonly HaushaltsbuchContext _context;

        public BuchungController(HaushaltsbuchContext context)
        {
            _context = context;
        }

        [HttpGet("GetBuchungenByMonth/{year}/{month}")]
        public async Task<ActionResult<IEnumerable<Buchung>>> GetBuchungenByMonth(int year, int month)
        {
            return await _context.Buchungen
                .Include(x => x.Kategorie)
                .Where(x => x.Buchungstag.Year == year && x.Buchungstag.Month == month)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Buchung>> GetBuchung(long id)
        {
            var buchung = await _context.Buchungen
                .Include(x => x.Kategorie)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (buchung == null)
            {
                return NotFound();
            }

            return buchung;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuchung(long id, Buchung buchung)
        {
            if (id != buchung.Id)
            {
                return BadRequest();
            }

            _context.Entry(buchung).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuchungExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Buchung>> PostBuchung(Buchung buchung)
        {
            _context.Buchungen.Add(buchung);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuchung", new { id = buchung.Id }, buchung);
        }

        // DELETE: api/Buchung/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Buchung>> DeleteBuchung(long id)
        {
            var buchung = await _context.Buchungen.FindAsync(id);
            if (buchung == null)
            {
                return NotFound();
            }

            _context.Buchungen.Remove(buchung);
            await _context.SaveChangesAsync();

            return buchung;
        }

        private bool BuchungExists(long id)
        {
            return _context.Buchungen.Any(e => e.Id == id);
        }
    }
}
