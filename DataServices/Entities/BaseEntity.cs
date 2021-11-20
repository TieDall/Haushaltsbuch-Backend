using System;

namespace DataServices.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime Changed { get; set; }
        public DateTime Created { get; set; }
    }
}
