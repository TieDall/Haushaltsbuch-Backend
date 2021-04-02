using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.ViewModels
{
    public class DauerauftragGrouped
    {
        public string Bezeichnung { get; set; }
        public Kategorie Kategorie { get; set; }
        public decimal CurrentBetrag { get; set; }
        public bool IsAktiv { get; set; }
        public bool HasMehrfachAktive { get; set; }
    }
}
