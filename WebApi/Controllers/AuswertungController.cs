using Microsoft.AspNetCore.Mvc;
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
            decimal result = 0;

            // Ermittle konfiguriertes Startkapital
            var startKapital = decimal.Parse(_context.Konfigurationen.FirstOrDefault(x => x.Parameter.Equals("Start")).Wert);

            // Ermittle Summe Buchungen
            var buchungen = _context.Buchungen.Where(x => x.Buchungstag <= DateTime.Now);
            var sumBuchungEinnahme = buchungen.Where(x => x.IsEinnahme).Sum(x => x.Betrag);
            var sumBuchungAusgabe = buchungen.Where(x => !x.IsEinnahme).Sum(x => x.Betrag);

            // Ermittle Summe Daueraufträge
            var dauerauftraege = _context.Dauerauftraege.Where(x => x.Beginn <= DateTime.Now);
            // Ermittle Summe monatliche Daueraufträge
            var monatlicheDauerauftraege = dauerauftraege.Where(x => x.Intervall == Enums.Intervall.monatlich);
            decimal sumMonatlicheDauerauftraege = 0;
            foreach (var item in monatlicheDauerauftraege)
            {
                var start = item.Beginn;
                while (item.Beginn <= DateTime.Now && (item.Ende == null || item.Beginn <= item.Ende))
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
            }
            // Ermittle Summe quartalsweise Daueraufträge
            var quartalsweiseDauerauftraege = dauerauftraege.Where(x => x.Intervall == Enums.Intervall.quartalsweise);
            decimal sumQuartalsweiseDauerauftraege = 0;
            foreach (var item in monatlicheDauerauftraege)
            {
                var start = item.Beginn;
                while (item.Beginn <= DateTime.Now && (item.Ende == null || item.Beginn <= item.Ende))
                {
                    if (item.IsEinnahme)
                    {
                        sumQuartalsweiseDauerauftraege += item.Betrag;
                    }
                    else
                    {
                        sumQuartalsweiseDauerauftraege -= item.Betrag;
                    }

                    item.Beginn = item.Beginn.AddMonths(6);
                }
            }
            // Ermittle Summe jährliche Daueraufträge
            var jaehrlicheDauerauftraege = dauerauftraege.Where(x => x.Intervall == Enums.Intervall.jaehrlich);
            decimal sumJaehrlicheDauerauftraege = 0;
            foreach (var item in jaehrlicheDauerauftraege)
            {
                var start = item.Beginn;
                while (item.Beginn <= DateTime.Now && (item.Ende == null || item.Beginn <= item.Ende))
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
            }

            result =
                startKapital
                + sumBuchungEinnahme - sumBuchungAusgabe
                + sumMonatlicheDauerauftraege + sumQuartalsweiseDauerauftraege + sumJaehrlicheDauerauftraege;

            return result;
        }

        [HttpGet("GetBilanz/{beginnYear}/{beginnMonth}/{endYear}/{endMonth}")]
        public async Task<ActionResult<decimal>> GetBilanz(int beginnYear, int beginnMonth, int endYear, int endMonth) { return null; }

        [HttpGet("GetVermoegen/{beginnYear}/{beginnMonth}/{endYear}/{endMonth}")]
        public async Task<ActionResult<decimal>> GetVermoegen(int beginnYear, int beginnMonth, int endYear, int endMonth) { return null; }

    }
}
