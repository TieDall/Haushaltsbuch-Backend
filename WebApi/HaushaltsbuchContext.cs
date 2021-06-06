using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).Changed = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).Created = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).Changed = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).Created = DateTime.Now;
                }
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).Changed = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).Created = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).Changed = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).Created = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
