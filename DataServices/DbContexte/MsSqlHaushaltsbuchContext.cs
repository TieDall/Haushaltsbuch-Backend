using Microsoft.EntityFrameworkCore;

namespace DataServices.DbContexte
{
    public class MsSqlHaushaltsbuchContext : HaushaltsbuchContext
    {
        public MsSqlHaushaltsbuchContext() { }
        public MsSqlHaushaltsbuchContext(DbContextOptions<MsSqlHaushaltsbuchContext> options) : base(options) { }
    }
}
