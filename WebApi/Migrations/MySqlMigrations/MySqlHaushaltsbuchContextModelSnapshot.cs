﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi;

namespace WebApi.Migrations.MySqlMigrations
{
    [DbContext(typeof(MySqlHaushaltsbuchContext))]
    partial class MySqlHaushaltsbuchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("WebApi.Entities.Buchung", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<decimal>("Betrag")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("text");

                    b.Property<DateTime>("Buchungstag")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsEinnahme")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("KategorieId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("KategorieId");

                    b.ToTable("Buchungen");
                });

            modelBuilder.Entity("WebApi.Entities.Dauerauftrag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Beginn")
                        .HasColumnType("datetime");

                    b.Property<decimal>("Betrag")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Ende")
                        .HasColumnType("datetime");

                    b.Property<int>("Intervall")
                        .HasColumnType("int");

                    b.Property<bool>("IsEinnahme")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("KategorieId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("KategorieId");

                    b.ToTable("Dauerauftraege");
                });

            modelBuilder.Entity("WebApi.Entities.Gutschein", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Ablaufdatum")
                        .HasColumnType("datetime");

                    b.Property<string>("Bemerkung")
                        .HasColumnType("text");

                    b.Property<decimal>("Betrag")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Gutscheine");
                });

            modelBuilder.Entity("WebApi.Entities.Kategorie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("text");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<bool>("IsEinnahme")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Kategorien");
                });

            modelBuilder.Entity("WebApi.Entities.Konfiguration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Parameter")
                        .HasColumnType("text");

                    b.Property<string>("Wert")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Konfigurationen");
                });

            modelBuilder.Entity("WebApi.Entities.Report", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("WebApi.Entities.ReportItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Config")
                        .HasColumnType("text");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<long>("ReportRowId")
                        .HasColumnType("bigint");

                    b.Property<int?>("ReportWidget")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReportRowId");

                    b.ToTable("ReportItems");
                });

            modelBuilder.Entity("WebApi.Entities.ReportRow", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<long>("ReportId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportRows");
                });

            modelBuilder.Entity("WebApi.Entities.Ruecklage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("text");

                    b.Property<decimal>("Summe")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.ToTable("Ruecklagen");
                });

            modelBuilder.Entity("WebApi.Entities.Buchung", b =>
                {
                    b.HasOne("WebApi.Entities.Kategorie", "Kategorie")
                        .WithMany()
                        .HasForeignKey("KategorieId");

                    b.Navigation("Kategorie");
                });

            modelBuilder.Entity("WebApi.Entities.Dauerauftrag", b =>
                {
                    b.HasOne("WebApi.Entities.Kategorie", "Kategorie")
                        .WithMany()
                        .HasForeignKey("KategorieId");

                    b.Navigation("Kategorie");
                });

            modelBuilder.Entity("WebApi.Entities.ReportItem", b =>
                {
                    b.HasOne("WebApi.Entities.ReportRow", "ReportRow")
                        .WithMany("ReportItems")
                        .HasForeignKey("ReportRowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReportRow");
                });

            modelBuilder.Entity("WebApi.Entities.ReportRow", b =>
                {
                    b.HasOne("WebApi.Entities.Report", "Report")
                        .WithMany("ReportRows")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("WebApi.Entities.Report", b =>
                {
                    b.Navigation("ReportRows");
                });

            modelBuilder.Entity("WebApi.Entities.ReportRow", b =>
                {
                    b.Navigation("ReportItems");
                });
#pragma warning restore 612, 618
        }
    }
}
