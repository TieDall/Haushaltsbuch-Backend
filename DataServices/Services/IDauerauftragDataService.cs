using DataServices.Services.Base;
using System.Collections.Generic;

namespace DataServices.Services
{
    public interface IDauerauftragDataService : IDataService<BusinessModels.Dauerauftrag, Entities.Dauerauftrag>
    {
        /// <summary>
        /// Gibt die Daueraufträge gruppiert nach Bezeichnung und Kategorie zurück.
        /// Enthält zusätzlich Informationen über:
        ///  - den aktuellen Betrag
        ///  - Flag, ob Dauerauftrag im aktuellen Monat relevant ist
        ///  - Flag, ob es mehr als einen Dauerauftrag gibt, der im aktuellem Monat relevant ist
        /// </summary>
        /// <returns></returns>
        List<BusinessModels.DauerauftragGrouped> GetGroupedByBezeichnungAndKategorie();

        List<BusinessModels.Dauerauftrag> GetByMonth(int year, int month);

        /// <summary>
        /// Gibt die Daueraufträge zu einer bestimmten Bezeichnung und Kategorie zurück.
        /// </summary>
        /// <param name="bezeichnung"></param>
        /// <param name="kategorieId"></param>
        /// <returns></returns>
        List<BusinessModels.Dauerauftrag> GetByBezeichnungAndKategorie(string bezeichnung, long kategorieId);
    }
}
