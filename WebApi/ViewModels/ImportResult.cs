using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels
{
    public class ImportResult
    {
        public int AnzahlKategorien { get; set; }
        public int AnzahlBuchungen { get; set; }
        public int AnzahlDauerauftraege { get; set; }
        public int AnzahlGutscheine { get; set; }
        public int AnzahlRuecklagen { get; set; }
    }
}
