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
    public class GutscheinController : ControllerBase
    {
        private readonly HaushaltsbuchContext _context;

        public GutscheinController(HaushaltsbuchContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gutschein>>> GetGutscheine()
        {
            return await _context.Gutscheine.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gutschein>> GetGutschein(long id)
        {
            var gutschein = await _context.Gutscheine.FindAsync(id);

            if (gutschein == null)
            {
                return NotFound();
            }

            return gutschein;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGutschein(long id, Gutschein gutschein)
        {
            if (id != gutschein.Id)
            {
                return BadRequest();
            }

            _context.Entry(gutschein).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GutscheinExists(id))
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
        public async Task<ActionResult<Gutschein>> PostGutschein(Gutschein gutschein)
        {
            _context.Gutscheine.Add(gutschein);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGutscheine", new { id = gutschein.Id }, gutschein);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Gutschein>> DeleteGutschein(long id)
        {
            var gutschein = await _context.Gutscheine.FindAsync(id);
            if (gutschein == null)
            {
                return NotFound();
            }

            _context.Gutscheine.Remove(gutschein);
            await _context.SaveChangesAsync();

            return gutschein;
        }

        private bool GutscheinExists(long id)
        {
            return _context.Gutscheine.Any(e => e.Id == id);
        }
    }
}
