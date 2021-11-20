using System;

namespace DataServices.Entities
{
    public class Gutschein : BaseEntity
    {
        public string Bezeichnung { get; set; }
        public decimal Betrag { get; set; }
        public DateTime? Ablaufdatum { get; set; }
        public string Bemerkung { get; set; }
    }
}
