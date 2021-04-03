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

    }
}
