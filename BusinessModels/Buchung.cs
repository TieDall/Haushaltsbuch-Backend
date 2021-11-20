using System;

namespace BusinessModels
{
    public class Buchung : BaseModel
    {
        public string Bezeichnung { get; set; }
        public decimal Betrag { get; set; }
        public DateTime Buchungstag { get; set; }
        public bool IsEinnahme { get; set; }

        public long? KategorieId { get; set; }
        public Kategorie Kategorie { get; set; }
    }
}
