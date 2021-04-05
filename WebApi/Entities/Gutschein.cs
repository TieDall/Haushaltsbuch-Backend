using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Gutschein : BaseEntity
    {
        public string Bezeichnung { get; set; }
        public decimal Betrag { get; set; }
        public DateTime? Ablaufdatum { get; set; }
        public string Bemerkung { get; set; }
    }
}
