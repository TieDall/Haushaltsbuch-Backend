using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Kategorie : BaseEntity
    {
        public string Bezeichnung { get; set; }
        public bool IsEinnahme { get; set; }
        public string Icon { get; set; }
        public ICollection<Buchung> Buchungen { get; set; }
    }
}
