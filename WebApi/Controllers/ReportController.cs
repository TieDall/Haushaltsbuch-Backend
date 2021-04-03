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
    public class ReportController : ControllerBase
    {
        private readonly HaushaltsbuchContext _context;

        public ReportController(HaushaltsbuchContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            return await _context.Reports.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(long id)
        {
            var report = await _context.Reports
                .Include(x => x.ReportRows)
                .ThenInclude(x => x.ReportItems)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Report>> DeleteReport(long id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return report;
        }

        [HttpPost("{name}")]
        public async Task<ActionResult<Report>> CreateReport(string name) 
        {
            var report = new Report();
            report.Bezeichnung = name;

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReport", new { id = report.Id }, report);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(long id, [FromBody] Report report)
        {
            if (id != report.Id)
            {
                return BadRequest();
            }

            var deletedRows = new List<ReportRow>();
            var reportRowEntities = _context.ReportRows.Where(x => x.ReportId == report.Id);

            // delete rows
            foreach (var item in reportRowEntities) 
            {
                if (!report.ReportRows.Any(x => x.Id == item.Id))
                {
                    deletedRows.Add(item);
                }
            }

            if (deletedRows.Count > 0)
            {
                _context.ReportRows.RemoveRange(deletedRows);
            }

            // detach not deleted rows
            foreach (var item in reportRowEntities)
            {
                if (!deletedRows.Any(x => x.Id == item.Id))
                {
                    _context.Entry(item).State = EntityState.Detached;
                }
            }

            _context.Reports.Update(report);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
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

        private bool ReportExists(long id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }

    }
}
