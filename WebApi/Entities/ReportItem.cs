using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Enums;

namespace WebApi.Entities
{
    public class ReportItem : BaseEntity
    {
        public int Position { get; set; }

        [ForeignKey("ReportRow")]
        public long ReportRowId { get; set; }
        public ReportRow ReportRow { get; set; }

        public string Config { get; set; }

        public ReportWidget? ReportWidget { get; set; }
    }
}
