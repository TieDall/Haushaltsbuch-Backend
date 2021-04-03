using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi
{
    public class HaushaltsbuchContext : DbContext
    {
        public DbSet<Kategorie> Kategorien { get; set; }
        public DbSet<Buchung> Buchungen { get; set; }
        public DbSet<Dauerauftrag> Dauerauftraege { get; set; }
        public DbSet<Konfiguration> Konfigurationen { get; set; }

        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportRow> ReportRows { get; set; }
        public DbSet<ReportItem> ReportItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Haushaltsbuch;Integrated Security=True");
        }
    }
}
