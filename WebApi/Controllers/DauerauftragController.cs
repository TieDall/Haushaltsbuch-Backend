using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DauerauftragController : ControllerBase
    {
        private readonly HaushaltsbuchContext _context;

        public DauerauftragController(HaushaltsbuchContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gibt die Daueraufträge gruppiert nach Bezeichnung und Kategorie zurück.
        /// Enthält zusätzlich Informationen über:
        ///  - den aktuellen Betrag
        ///  - Flag, ob Dauerauftrag im aktuellen Monat relevant ist
        ///  - Flag, ob es mehr als einen Dauerauftrag gibt, der im aktuellem Monat relevant ist
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDauerauftraegeGrouped")]
        public async Task<ActionResult<IEnumerable<Dauerauftrag>>> GetDauerauftraegeGrouped()
        {
            List<DauerauftragGrouped> result = new List<DauerauftragGrouped>();

            var dauerauftraege = await _context.Dauerauftraege
                .Include(x => x.Kategorie)
                .ToListAsync();

            var groupedDauerauftraege = dauerauftraege
                .GroupBy(x => new { x.Bezeichnung, x.Kategorie })
                .Select(x => x.Key)
                .ToList();

            foreach (var item in groupedDauerauftraege)
            {
                var now = DateTime.Now;

                var beginnMonat = new DateTime(now.Year, now.Month, 1);
                var endeMonat = beginnMonat.AddMonths(1).AddDays(-1);

                var currentQuartal = (now.Month - 1) / 3 + 1;
                var beginnQuartal = new DateTime(now.Year, (currentQuartal - 1) * 3 + 1, 1);
                var endeQuartal = beginnQuartal.AddMonths(3).AddDays(-1);

                var beginnJahr = new DateTime(now.Year, 1, 1);
                var endeJahr = beginnJahr.AddYears(1).AddDays(-1);

                var dauerauftraegeByBezeichnungKategorie = dauerauftraege.Where(x => 
                    x.Bezeichnung == item.Bezeichnung && 
                    x.KategorieId == item.Kategorie.Id);

                /*
                 * Filterung bezieht sich immer auf den vollen Monat.
                 */
                var aktiveDauerauftraege = dauerauftraegeByBezeichnungKategorie.Where(x => 
                    (x.Intervall == Enums.Intervall.monatlich &&
                        /* Beginn */
                        x.Beginn <= beginnMonat
                        &&
                        /* Ende */
                        (
                            x.Ende == null 
                            ||
                            (
                                x.Ende != null &&
                                x.Ende.Value >= endeMonat
                            )
                        )
                    )
                    ||
                    (x.Intervall == Enums.Intervall.quartalsweise &&
                        /* Beginn */
                        x.Beginn <= beginnQuartal
                        &&
                        /* Ende */
                        (
                            x.Ende == null 
                            || 
                            (
                                x.Ende != null && 
                                x.Ende.Value >= endeQuartal
                            )
                        )
                    )
                    ||
                    (x.Intervall == Enums.Intervall.jaehrlich &&
                        /* Beginn */
                        x.Beginn >= beginnJahr
                        &&
                        /* Ende */
                        (
                            x.Ende == null
                            ||
                            (
                                x.Ende != null &&
                                x.Ende.Value <= endeJahr
                            )
                        )
                    ));

                result.Add(new DauerauftragGrouped()
                {
                    Bezeichnung = item.Bezeichnung,
                    Kategorie = item.Kategorie,
                    CurrentBetrag = aktiveDauerauftraege.Sum(x => x.Betrag),
                    IsAktiv = aktiveDauerauftraege.Any(x => (x.Ende == null || x.Ende > DateTime.Now)),
                    HasMehrfachAktive = aktiveDauerauftraege.Count() > 1
                });
            }

            return Ok(result);
        }

        [HttpGet("GetDauerauftraegeByMonth/{year}/{month}")]
        public async Task<ActionResult<IEnumerable<Dauerauftrag>>> GetDauerauftraegeByMonth(int year, int month)
        {
            var monthStart = new DateTime(year, month, 1);
            var monthEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            return _context.Dauerauftraege
                .Include(x => x.Kategorie)
                .ToList()
                .Where(x =>
                    (
                        x.Intervall == Enums.Intervall.monatlich &&
                        DateTime.Compare(x.Beginn, monthEnd) <= 0 &&
                        (x.Ende == null || DateTime.Compare((DateTime)x.Ende, monthStart) >= 0)
                    )
                    ||
                    (
                        x.Intervall == Enums.Intervall.quartalsweise
                        && DateTime.Compare(x.Beginn, monthEnd) <= 0
                        && (x.Ende == null || DateTime.Compare((DateTime)x.Ende, monthStart) >= 0)
                        &&
                        (
                            ((x.Beginn.Month == 1 || x.Beginn.Month == 4 || x.Beginn.Month == 7 || x.Beginn.Month == 10)
                            &&
                            (month == 1 || month == 4 || month == 7 || month == 10))
                            ||
                            ((x.Beginn.Month == 2 || x.Beginn.Month == 5 || x.Beginn.Month == 8 || x.Beginn.Month == 11)
                            &&
                            (month == 2 || month == 5 || month == 8 || month == 11))
                            ||
                            ((x.Beginn.Month == 3 || x.Beginn.Month == 6 || x.Beginn.Month == 9 || x.Beginn.Month == 12)
                            &&
                            (month == 3 || month == 6 || month == 9 || month == 12))
                        )
                    )
                    ||
                    (
                        x.Intervall == Enums.Intervall.jaehrlich
                        && DateTime.Compare(x.Beginn, monthEnd) <= 0
                        && (x.Ende == null || DateTime.Compare((DateTime)x.Ende, monthStart) >= 0)
                        && x.Beginn.Month == month
                    )
                )
                .ToList();
        }

        /// <summary>
        /// Gibt die Daueraufträge zu einer Bezeichnung und Kategorie zurück.
        /// </summary>
        /// <param name="bezeichnung"></param>
        /// <param name="kategorieId"></param>
        /// <returns></returns>
        [HttpGet("GetDauerauftraege/{bezeichnung}/{kategorieId}")]
        public async Task<ActionResult<IEnumerable<Dauerauftrag>>> GetDauerauftraegeByBezeichnungKategorie(string bezeichnung, long kategorieId)
        {
            return await _context.Dauerauftraege
                .Include(x => x.Kategorie)
                .Where(x => x.Bezeichnung == bezeichnung && x.KategorieId == kategorieId)
                .OrderBy(x => x.Ende)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dauerauftrag>> GetDauerauftrag(long id)
        {
            var dauerauftrag = await _context.Dauerauftraege.FindAsync(id);

            if (dauerauftrag == null)
            {
                return NotFound();
            }

            return dauerauftrag;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDauerauftrag(long id, Dauerauftrag dauerauftrag)
        {
            if (id != dauerauftrag.Id)
            {
                return BadRequest();
            }

            _context.Entry(dauerauftrag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DauerauftragExists(id))
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
        public async Task<ActionResult<Dauerauftrag>> PostDauerauftrag(Dauerauftrag dauerauftrag)
        {
            _context.Dauerauftraege.Add(dauerauftrag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDauerauftrag", new { id = dauerauftrag.Id }, dauerauftrag);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Dauerauftrag>> DeleteDauerauftrag(long id)
        {
            var dauerauftrag = await _context.Dauerauftraege.FindAsync(id);
            if (dauerauftrag == null)
            {
                return NotFound();
            }

            _context.Dauerauftraege.Remove(dauerauftrag);
            await _context.SaveChangesAsync();

            return dauerauftrag;
        }

        private bool DauerauftragExists(long id)
        {
            return _context.Dauerauftraege.Any(e => e.Id == id);
        }
    }
}
