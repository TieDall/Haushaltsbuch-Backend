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
    public class KategorieController : ControllerBase
    {
        private readonly HaushaltsbuchContext _context;

        public KategorieController(HaushaltsbuchContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kategorie>>> GetKategorien()
        {
            return await _context.Kategorien.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kategorie>> GetKategorie(long id)
        {
            var kategorie = await _context.Kategorien.FindAsync(id);

            if (kategorie == null)
            {
                return NotFound();
            }

            return kategorie;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutKategorie(long id, Kategorie kategorie)
        {
            if (id != kategorie.Id)
            {
                return BadRequest();
            }

            _context.Entry(kategorie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategorieExists(id))
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
        public async Task<ActionResult<Kategorie>> PostKategorie(Kategorie kategorie)
        {
            _context.Kategorien.Add(kategorie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKategorie", new { id = kategorie.Id }, kategorie);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Kategorie>> DeleteKategorie(long id)
        {
            var kategorie = await _context.Kategorien.FindAsync(id);
            if (kategorie == null)
            {
                return NotFound();
            }

            _context.Kategorien.Remove(kategorie);
            await _context.SaveChangesAsync();

            return kategorie;
        }

        private bool KategorieExists(long id)
        {
            return _context.Kategorien.Any(e => e.Id == id);
        }
    }
}
