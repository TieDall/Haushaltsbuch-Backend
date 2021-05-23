using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class MySqlHaushaltsbuchContext : HaushaltsbuchContext
    {
        public MySqlHaushaltsbuchContext() { }
        public MySqlHaushaltsbuchContext(DbContextOptions<HaushaltsbuchContext> options) : base(options) { }
    }
}
