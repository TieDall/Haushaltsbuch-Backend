using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuswertungController : ControllerBase
    {
        private readonly HaushaltsbuchContext _context;

        public AuswertungController(HaushaltsbuchContext context)
        {
            _context = context;
        }

        [HttpGet("GetVermoegen")]
        public async Task<ActionResult<decimal>> GetCurrentVermoegen()
        {
            return await CalcVermoegen();
        }

        [HttpGet("GetVermoegen/{beginnYear}/{beginnMonth}/{endYear}/{endMonth}")]
        public async Task<ActionResult<List<decimal>>> GetVermoegen(int beginnYear, int beginnMonth, int endYear, int endMonth) 
        {
            var result = new List<decimal>();

            while (beginnYear != endYear || beginnMonth != endMonth)
            {
                result.Add( await CalcVermoegen(beginnYear, beginnMonth));

                if (beginnMonth < 12)
                {
                    beginnMonth++;
                }
                else
                {
                    beginnYear++;
                    beginnMonth = 1;
                }
            }
            result.Add(await CalcVermoegen(beginnYear, beginnMonth));

            return result;
        }
                
        private async Task<decimal> CalcVermoegen(int? year = null, int? month = null)
        {
            decimal result = 0;

            // Set Current Month und Year
            var monthToReturn = new DateTime();
            if (year == null || month == null)
            {
                monthToReturn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            }
            else
            {
                monthToReturn = new DateTime((int)year, (int)month, DateTime.DaysInMonth((int)year, (int)month));
            }

            // Ermittle konfiguriertes Startkapital
            var startKapital = decimal.Parse(_context.Konfigurationen.FirstOrDefault(x => x.Parameter.Equals("Start")).Wert);

            // Ermittle Summe Buchungen
            var buchungen = _context.Buchungen.Where(x => x.Buchungstag <= monthToReturn);
            var sumBuchungEinnahme = buchungen.Where(x => x.IsEinnahme).Sum(x => x.Betrag);
            var sumBuchungAusgabe = buchungen.Where(x => !x.IsEinnahme).Sum(x => x.Betrag);

            // Ermittle Summe Daueraufträge
            var dauerauftraege =  await _context.Dauerauftraege.Where(x => x.Beginn <= monthToReturn).ToListAsync();
            // Ermittle Summe monatliche Daueraufträge
            var monatlicheDauerauftraege = dauerauftraege.Where(x => x.Intervall == Enums.Intervall.monatlich);
            decimal sumMonatlicheDauerauftraege = 0;
            foreach (var item in monatlicheDauerauftraege)
            {
                var start = item.Beginn;
                while (item.Beginn <= monthToReturn && (item.Ende == null || item.Beginn <= item.Ende))
                {
                    if (item.IsEinnahme)
                    {
                        sumMonatlicheDauerauftraege += item.Betrag;
                    }
                    else
                    {
                        sumMonatlicheDauerauftraege -= item.Betrag;
                    }

                    item.Beginn = item.Beginn.AddMonths(1);
                }
                item.Beginn = start;
            }
            // Ermittle Summe quartalsweise Daueraufträge
            var quartalsweiseDauerauftraege = dauerauftraege.Where(x => x.Intervall == Enums.Intervall.quartalsweise);
            decimal sumQuartalsweiseDauerauftraege = 0;
            foreach (var item in quartalsweiseDauerauftraege)
            {
                var start = item.Beginn;
                while (item.Beginn <= monthToReturn && (item.Ende == null || item.Beginn <= item.Ende))
                {
                    if (item.IsEinnahme)
                    {
                        sumQuartalsweiseDauerauftraege += item.Betrag;
                    }
                    else
                    {
                        sumQuartalsweiseDauerauftraege -= item.Betrag;
                    }

                    item.Beginn = item.Beginn.AddMonths(3);
                }
                item.Beginn = start;
            }
            // Ermittle Summe jährliche Daueraufträge
            var jaehrlicheDauerauftraege = dauerauftraege.Where(x => x.Intervall == Enums.Intervall.jaehrlich);
            decimal sumJaehrlicheDauerauftraege = 0;
            foreach (var item in jaehrlicheDauerauftraege)
            {
                var start = item.Beginn;
                while (item.Beginn <= monthToReturn && (item.Ende == null || item.Beginn <= item.Ende))
                {
                    if (item.IsEinnahme)
                    {
                        sumJaehrlicheDauerauftraege += item.Betrag;
                    }
                    else
                    {
                        sumJaehrlicheDauerauftraege -= item.Betrag;
                    }

                    item.Beginn = item.Beginn.AddMonths(12);
                }
                item.Beginn = start;
            }

            result =
                startKapital
                + sumBuchungEinnahme - sumBuchungAusgabe
                + sumMonatlicheDauerauftraege + sumQuartalsweiseDauerauftraege + sumJaehrlicheDauerauftraege;

            return result;
        }

        [HttpGet("GetBilanz")]
        public async Task<ActionResult<decimal>> GetCurrentBilanz() 
        {
            return await CalcBilanz();
        }

        [HttpGet("GetBilanz/{beginnYear}/{beginnMonth}/{endYear}/{endMonth}")]
        public async Task<ActionResult<List<decimal>>> GetBilanz(int beginnYear, int beginnMonth, int endYear, int endMonth)
        {
            var result = new List<decimal>();

            while (beginnYear != endYear || beginnMonth != endMonth)
            {
                result.Add(await CalcBilanz(beginnYear, beginnMonth));

                if (beginnMonth < 12)
                {
                    beginnMonth++;
                }
                else
                {
                    beginnYear++;
                    beginnMonth = 1;
                }
            }
            result.Add(await CalcBilanz(beginnYear, beginnMonth));

            return result;
        }

        private async Task<decimal> CalcBilanz(int? year = null, int? month = null)
        {
            decimal result = 0;

            // Set Current Month und Year
            var monthToReturn = new DateTime();
            if (year == null || month == null)
            {
                monthToReturn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            }
            else
            {
                monthToReturn = new DateTime((int)year, (int)month, DateTime.DaysInMonth((int)year, (int)month));
            }

            var monthStart = new DateTime(monthToReturn.Year, monthToReturn.Month, 1);
            var monthEnd = new DateTime(monthToReturn.Year, monthToReturn.Month, DateTime.DaysInMonth(monthToReturn.Year, monthToReturn.Month));

            // Ermittle Summe Buchungen
            var buchungen = _context.Buchungen.Where(x => x.Buchungstag.Month == monthToReturn.Month && x.Buchungstag.Year == monthToReturn.Year);
            var sumBuchungEinnahme = buchungen.Where(x => x.IsEinnahme).Sum(x => x.Betrag);
            var sumBuchungAusgabe = buchungen.Where(x => !x.IsEinnahme).Sum(x => x.Betrag);

            // Ermittle Summe Daueraufträge
            var dauerauftraege = await _context.Dauerauftraege
                .Include(x => x.Kategorie)
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
                .ToListAsync();

            var sumDauerauftragEinnahme = dauerauftraege.Where(x => x.IsEinnahme).Sum(x => x.Betrag);
            var sumDauerauftragAusgabe = dauerauftraege.Where(x => !x.IsEinnahme).Sum(x => x.Betrag);

            result = 
                sumBuchungEinnahme - sumBuchungAusgabe
                + sumDauerauftragEinnahme - sumDauerauftragAusgabe;

            return result;
        }
    }
}
