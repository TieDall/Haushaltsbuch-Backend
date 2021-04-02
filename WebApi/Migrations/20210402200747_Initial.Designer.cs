﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi;

namespace WebApi.Migrations
{
    [DbContext(typeof(HaushaltsbuchContext))]
    [Migration("20210402200747_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("WebApi.Entities.Buchung", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<decimal>("Betrag")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Buchungstag")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsEinnahme")
                        .HasColumnType("bit");

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
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Beginn")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Betrag")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("WebApi.Entities.Kategorie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Bezeichnung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEinnahme")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Kategorien");
                });

            modelBuilder.Entity("WebApi.Entities.Konfiguration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Parameter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Wert")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Konfigurationen");
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
#pragma warning restore 612, 618
        }
    }
}