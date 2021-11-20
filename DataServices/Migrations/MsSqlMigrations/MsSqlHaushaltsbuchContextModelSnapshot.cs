﻿// <auto-generated />
using System;
using DataServices.DbContexte;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataServices.Migrations.MsSqlMigrations
{
    [DbContext(typeof(MsSqlHaushaltsbuchContext))]
    partial class MsSqlHaushaltsbuchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataServices.Entities.Buchung", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Betrag")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Buchungstag")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsEinnahme")
                        .HasColumnType("bit");

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
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Beginn")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Betrag")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Ende")
                        .HasColumnType("datetime2");

                    b.Property<int>("Intervall")
                        .HasColumnType("int");

                    b.Property<bool>("IsEinnahme")
                        .HasColumnType("bit");

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
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Ablaufdatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Bemerkung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Betrag")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Gutscheine");
                });

            modelBuilder.Entity("DataServices.Entities.Kategorie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEinnahme")
                        .HasColumnType("bit");

                    b.Property<long?>("KategorieId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("KategorieId");

                    b.ToTable("Kategorien");
                });

            modelBuilder.Entity("DataServices.Entities.Konfiguration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Parameter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Wert")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Konfigurationen");
                });

            modelBuilder.Entity("DataServices.Entities.Ruecklage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Changed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Summe")
                        .HasColumnType("decimal(18,2)");

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
                        .WithMany()
                        .HasForeignKey("KategorieId");

                    b.Navigation("Kategorie");
                });

            modelBuilder.Entity("DataServices.Entities.Kategorie", b =>
                {
                    b.HasOne("DataServices.Entities.Kategorie", null)
                        .WithMany("Kategorien")
                        .HasForeignKey("KategorieId");
                });

            modelBuilder.Entity("DataServices.Entities.Kategorie", b =>
                {
                    b.Navigation("Buchungen");

                    b.Navigation("Kategorien");
                });
#pragma warning restore 612, 618
        }
    }
}
