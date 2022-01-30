﻿// <auto-generated />
using System;
using DataServices.DbContexte;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataServices.Migrations.MySqlMigrations
{
    [DbContext(typeof(MySqlHaushaltsbuchContext))]
    partial class MySqlHaushaltsbuchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("DataServices.Entities.Buchung", b =>
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

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsEinnahme")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("KategorieId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("KategorieId");

                    b.ToTable("Buchungen");
                });

            modelBuilder.Entity("DataServices.Entities.Dauerauftrag", b =>
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

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

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

            modelBuilder.Entity("DataServices.Entities.Gutschein", b =>
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

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Gutscheine");
                });

            modelBuilder.Entity("DataServices.Entities.Kategorie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("text");

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<bool>("IsEinnahme")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Kategorien");
                });

            modelBuilder.Entity("DataServices.Entities.Konfiguration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("Parameter")
                        .HasColumnType("text");

                    b.Property<string>("Wert")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Konfigurationen");
                });

            modelBuilder.Entity("DataServices.Entities.Ruecklage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("text");

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<decimal>("Summe")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.ToTable("Ruecklagen");
                });

            modelBuilder.Entity("DataServices.Entities.Buchung", b =>
                {
                    b.HasOne("DataServices.Entities.Kategorie", "Kategorie")
                        .WithMany("Buchungen")
                        .HasForeignKey("KategorieId");

                    b.Navigation("Kategorie");
                });

            modelBuilder.Entity("DataServices.Entities.Dauerauftrag", b =>
                {
                    b.HasOne("DataServices.Entities.Kategorie", "Kategorie")
                        .WithMany("Dauerauftraege")
                        .HasForeignKey("KategorieId");

                    b.Navigation("Kategorie");
                });

            modelBuilder.Entity("DataServices.Entities.Kategorie", b =>
                {
                    b.Navigation("Buchungen");

                    b.Navigation("Dauerauftraege");
                });
#pragma warning restore 612, 618
        }
    }
}
