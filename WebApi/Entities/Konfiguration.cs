using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Konfiguration : BaseEntity
    {
        public string Parameter { get; set; }
        public string Wert { get; set; }
    }
}
