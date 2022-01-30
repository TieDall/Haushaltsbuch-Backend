using AutoMapper;
using BusinessModels;
using DataServices.DbContexte;
using DataServices.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataServices.Services
{
    public class DauerauftragDataService : DataService<BusinessModels.Dauerauftrag, Entities.Dauerauftrag>, IDauerauftragDataService
    {
        private HaushaltsbuchContext _haushaltsbuchContext;
        private IMapper _mapper;

        public DauerauftragDataService(
            HaushaltsbuchContext haushaltsbuchContext, 
            IMapper mapper) : base(haushaltsbuchContext, mapper)
        {
            _haushaltsbuchContext = haushaltsbuchContext;
            _mapper = mapper;
        }

        public List<BusinessModels.Dauerauftrag> GetByBezeichnungAndKategorie(string bezeichnung, long kategorieId)
        {
            var entities = GetDefaultQuery()
                .Where(x => x.Bezeichnung == bezeichnung && x.KategorieId == kategorieId)
                .OrderBy(x => x.Ende)
                .ToList();

            return _mapper.Map<List<BusinessModels.Dauerauftrag>>(entities);
        }

        public List<BusinessModels.Dauerauftrag> GetByMonth(int year, int month)
        {
            var monthStart = new DateTime(year, month, 1);
            var monthEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            var entities = GetDefaultQuery()
                .ToList()
                .Where(x =>
                    (
                        x.Intervall == Common.Enums.Intervall.monatlich &&
                        DateTime.Compare(x.Beginn, monthEnd) <= 0 &&
                        (x.Ende == null || DateTime.Compare((DateTime)x.Ende, monthStart) >= 0)
                    )
                    ||
                    (
                        x.Intervall == Common.Enums.Intervall.quartalsweise
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
                        x.Intervall == Common.Enums.Intervall.jaehrlich
                        && DateTime.Compare(x.Beginn, monthEnd) <= 0
                        && (x.Ende == null || DateTime.Compare((DateTime)x.Ende, monthStart) >= 0)
                        && x.Beginn.Month == month
                    )
                )
                .ToList();

            return entities.Any() ? _mapper.Map<List<BusinessModels.Dauerauftrag>>(entities) : null;
        }

        public List<BusinessModels.DauerauftragGrouped> GetGroupedByBezeichnungAndKategorie()
        {
            List<DauerauftragGrouped> result = new List<DauerauftragGrouped>();

            var dauerauftraege = _haushaltsbuchContext.Dauerauftraege
                .Include(x => x.Kategorie)
                .ToList();

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
                    (x.Intervall == Common.Enums.Intervall.monatlich &&
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
                    (x.Intervall == Common.Enums.Intervall.quartalsweise &&
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
                    (x.Intervall == Common.Enums.Intervall.jaehrlich &&
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
                    Kategorie = _mapper.Map<Kategorie>(item.Kategorie),
                    CurrentBetrag = aktiveDauerauftraege.Sum(x => x.Betrag),
                    IsAktiv = aktiveDauerauftraege.Any(x => (x.Ende == null || x.Ende > DateTime.Now)),
                    HasMehrfachAktive = aktiveDauerauftraege.Count() > 1
                });
            }
            return result;
        }

        public override IQueryable<Entities.Dauerauftrag> GetDefaultQuery()
        {
            return _haushaltsbuchContext.Set<Entities.Dauerauftrag>().Include(x => x.Kategorie);
        }
    }
}
