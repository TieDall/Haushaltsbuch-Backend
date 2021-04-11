using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Ruecklage : BaseEntity
    {
        public string Bezeichnung { get; set; }
        public decimal Summe { get; set; }
    }
}
