using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Buchung : BaseEntity
    {
        public string Bezeichnung { get; set; }
        public decimal Betrag { get; set; }
        public DateTime Buchungstag { get; set; }
        public bool IsEinnahme { get; set; }

        [ForeignKey("Kategorie")]
        public long? KategorieId { get; set; }
        public Kategorie Kategorie { get; set; }
    }
}
