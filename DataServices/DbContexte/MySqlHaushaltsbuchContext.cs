using Microsoft.EntityFrameworkCore;

namespace DataServices.DbContexte
{
    public class MySqlHaushaltsbuchContext : HaushaltsbuchContext
    {
        public MySqlHaushaltsbuchContext() { }
        public MySqlHaushaltsbuchContext(DbContextOptions<MySqlHaushaltsbuchContext> options) : base(options) { }
    }
}
