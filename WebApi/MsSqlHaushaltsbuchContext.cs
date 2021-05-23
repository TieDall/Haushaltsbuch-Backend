using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class MsSqlHaushaltsbuchContext : HaushaltsbuchContext
    {
        public MsSqlHaushaltsbuchContext() { }
        public MsSqlHaushaltsbuchContext(DbContextOptions<MsSqlHaushaltsbuchContext> options) : base(options) { }
    }
}
