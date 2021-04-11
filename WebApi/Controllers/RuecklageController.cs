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
    public class RuecklageController : ControllerBase
    {
        private readonly HaushaltsbuchContext _context;

        public RuecklageController(HaushaltsbuchContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ruecklage>>> GetRuecklagen()
        {
            return await _context.Ruecklagen.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ruecklage>> GetRuecklage(long id)
        {
            var ruecklage = await _context.Ruecklagen.FindAsync(id);

            if (ruecklage == null)
            {
                return NotFound();
            }

            return ruecklage;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRuecklage(long id, Ruecklage ruecklage)
        {
            if (id != ruecklage.Id)
            {
                return BadRequest();
            }

            _context.Entry(ruecklage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuecklageExists(id))
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
        public async Task<ActionResult<Ruecklage>> PostGutschein(Ruecklage ruecklage)
        {
            _context.Ruecklagen.Add(ruecklage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRuecklage", new { id = ruecklage.Id }, ruecklage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Ruecklage>> DeleteRuecklage(long id)
        {
            var ruecklage = await _context.Ruecklagen.FindAsync(id);
            if (ruecklage == null)
            {
                return NotFound();
            }

            _context.Ruecklagen.Remove(ruecklage);
            await _context.SaveChangesAsync();

            return ruecklage;
        }

        private bool RuecklageExists(long id)
        {
            return _context.Ruecklagen.Any(e => e.Id == id);
        }
    }
}
