using Common.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataServices.Entities
{
    public class Dauerauftrag : BaseEntity
    {
        public string Bezeichnung { get; set; }
        public decimal Betrag { get; set; }
        public bool IsEinnahme { get; set; }
        public Intervall Intervall { get; set; }
        public DateTime Beginn { get; set; }
        public DateTime? Ende { get; set; }

        [ForeignKey("Kategorie")]
        public long? KategorieId { get; set; }
        public Kategorie Kategorie { get; set; }
    }
}
