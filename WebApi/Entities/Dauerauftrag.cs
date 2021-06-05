using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Enums;

namespace WebApi.Entities
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

        public bool IsAktiv => (Beginn <= DateTime.Now) && (Ende == null || DateTime.Now <= Ende);
    }
}
