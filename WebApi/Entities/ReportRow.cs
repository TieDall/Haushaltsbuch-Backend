using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class ReportRow : BaseEntity
    {
        public int Position { get; set; }

        [ForeignKey("Report")]
        public long ReportId { get; set; }
        public Report Report { get; set; }

        public ICollection<ReportItem> ReportItems { get; set; }
    }
}
