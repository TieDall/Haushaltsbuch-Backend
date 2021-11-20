using Common.Enums;
using System;

namespace BusinessModels
{
    public class Dauerauftrag : BaseModel
    {
        public string Bezeichnung { get; set; }
        public decimal Betrag { get; set; }
        public bool IsEinnahme { get; set; }
        public Intervall Intervall { get; set; }
        public DateTime Beginn { get; set; }
        public DateTime? Ende { get; set; }

        public long? KategorieId { get; set; }
        public Kategorie Kategorie { get; set; }

        public bool IsAktiv => (Beginn <= DateTime.Now) && (Ende == null || DateTime.Now <= Ende);
    }
}
