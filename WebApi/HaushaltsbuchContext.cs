using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi
{
    /**
     * Add-Migration [...] -Context MsSqlHaushaltsbuchContext -OutputDir Migrations\MsSqlMigrations
     * Add-Migration [...] -Context MySqlHaushaltsbuchContext -OutputDir Migrations\MySqlMigrations
     */
    public class HaushaltsbuchContext : DbContext
    {
        public HaushaltsbuchContext() { }
        public HaushaltsbuchContext(DbContextOptions<HaushaltsbuchContext> options) : base(options) { }
        protected HaushaltsbuchContext(DbContextOptions options) : base(options) { }

        public DbSet<Kategorie> Kategorien { get; set; }
        public DbSet<Buchung> Buchungen { get; set; }
        public DbSet<Dauerauftrag> Dauerauftraege { get; set; }
        public DbSet<Konfiguration> Konfigurationen { get; set; }

        public DbSet<Gutschein> Gutscheine { get; set; }
        public DbSet<Ruecklage> Ruecklagen { get; set; }

        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportRow> ReportRows { get; set; }
        public DbSet<ReportItem> ReportItems { get; set; }
    }
}
