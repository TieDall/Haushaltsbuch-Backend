using System.Collections.Generic;

namespace BusinessModels
{
    public class Kategorie : BaseModel
    {
        public string Bezeichnung { get; set; }
        public bool IsEinnahme { get; set; }
        public string Icon { get; set; }

        public ICollection<Buchung> Buchungen { get; set; }
        public ICollection<Kategorie> Kategorien { get; set; }
    }
}
