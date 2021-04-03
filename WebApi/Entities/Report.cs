using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Report : BaseEntity
    {
        public string Bezeichnung { get; set; }
        public ICollection<ReportRow> ReportRows { get; set; }
    }
}
