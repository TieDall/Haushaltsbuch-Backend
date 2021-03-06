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
    public class KonfigurationController : ControllerBase
    {
        private readonly HaushaltsbuchContext _context;

        public KonfigurationController(HaushaltsbuchContext context)
        {
            _context = context;
        }

        [HttpPost("ResetDatabase")]
        public async Task ResetDatabase()
        {
            _context.Buchungen.RemoveRange(_context.Buchungen.ToList());
            _context.Dauerauftraege.RemoveRange(_context.Dauerauftraege.ToList());
            _context.Kategorien.RemoveRange(_context.Kategorien.ToList());

            _context.Gutscheine.RemoveRange(_context.Gutscheine.ToList());
            _context.Ruecklagen.RemoveRange(_context.Ruecklagen.ToList());

            _context.ReportItems.RemoveRange(_context.ReportItems.ToList());
            _context.ReportRows.RemoveRange(_context.ReportRows.ToList());
            _context.Reports.RemoveRange(_context.Reports.ToList());

            _context.Konfigurationen.RemoveRange(_context.Konfigurationen.ToList());
            await _context.Konfigurationen.AddAsync(new Konfiguration() { Parameter = "Start", Wert = "0" });

            await _context.SaveChangesAsync();
        }

        #region CRUD

        // GET: api/Konfiguration
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Konfiguration>>> GetKonfigurationen()
        {
            return await _context.Konfigurationen.ToListAsync();
        }

        // GET: api/Konfiguration/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Konfiguration>> GetKonfiguration(long id)
        {
            var konfiguration = await _context.Konfigurationen.FindAsync(id);

            if (konfiguration == null)
            {
                return NotFound();
            }

            return konfiguration;
        }

        // PUT: api/Konfiguration/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKonfiguration(long id, [FromBody] Konfiguration konfiguration)
        {
            if (id != konfiguration.Id)
            {
                return BadRequest();
            }

            _context.Entry(konfiguration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KonfigurationExists(id))
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

        // POST: api/Konfiguration
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Konfiguration>> PostKonfiguration(Konfiguration konfiguration)
        {
            _context.Konfigurationen.Add(konfiguration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKonfiguration", new { id = konfiguration.Id }, konfiguration);
        }

        // DELETE: api/Konfiguration/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Konfiguration>> DeleteKonfiguration(long id)
        {
            var konfiguration = await _context.Konfigurationen.FindAsync(id);
            if (konfiguration == null)
            {
                return NotFound();
            }

            _context.Konfigurationen.Remove(konfiguration);
            await _context.SaveChangesAsync();

            return konfiguration;
        }

        #endregion

        private bool KonfigurationExists(long id)
        {
            return _context.Konfigurationen.Any(e => e.Id == id);
        }
    }
}
