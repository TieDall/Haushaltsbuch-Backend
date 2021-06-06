using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime Changed { get; set; }
        public DateTime Created { get; set; }
    }
}
