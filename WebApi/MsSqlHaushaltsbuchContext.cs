using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class MsSqlHaushaltsbuchContext : HaushaltsbuchContext
    {
        public MsSqlHaushaltsbuchContext(DbContextOptions<HaushaltsbuchContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Haushaltsbuch;Integrated Security=True");
        }
    }
}
