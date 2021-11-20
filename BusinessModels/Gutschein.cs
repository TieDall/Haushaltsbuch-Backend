using System;

namespace BusinessModels
{
    public class Gutschein : BaseModel
    {
        public string Bezeichnung { get; set; }
        public decimal Betrag { get; set; }
        public DateTime? Ablaufdatum { get; set; }
        public string Bemerkung { get; set; }

        public bool IsOneMonthLeft => this.Ablaufdatum?.AddMonths(-1) < DateTime.Now;
        public bool IsHalfYearLeft => this.Ablaufdatum?.AddMonths(-6) < DateTime.Now;
    }
}
