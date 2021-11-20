using System;

namespace BusinessModels
{
    public class BaseModel
    {
        public long Id { get; set; }

        public DateTime Changed { get; set; }
        public DateTime Created { get; set; }
    }
}
