using System.Collections.Generic;

namespace DataServices.Entities
{
    public class Kategorie : BaseEntity
    {
        public string Bezeichnung { get; set; }
        public bool IsEinnahme { get; set; }
        public string Icon { get; set; }

        public ICollection<Buchung> Buchungen { get; set; }
        public ICollection<Dauerauftrag> Dauerauftraege { get; set; }
    }
}
