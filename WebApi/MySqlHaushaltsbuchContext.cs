using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class MySqlHaushaltsbuchContext : HaushaltsbuchContext
    {
        public MySqlHaushaltsbuchContext(DbContextOptions<HaushaltsbuchContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"server=ip-adress;database=Haushaltsbuch;user=username;password=password");
        }
    }
}
