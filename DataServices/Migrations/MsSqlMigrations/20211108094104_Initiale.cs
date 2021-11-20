using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataServices.Migrations.MsSqlMigrations
{
    public partial class Initiale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gutscheine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Betrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ablaufdatum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Bemerkung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Changed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gutscheine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategorien",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEinnahme = table.Column<bool>(type: "bit", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategorieId = table.Column<long>(type: "bigint", nullable: true),
                    Changed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kategorien_Kategorien_KategorieId",
                        column: x => x.KategorieId,
                        principalTable: "Kategorien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Konfigurationen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Parameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wert = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Changed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konfigurationen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ruecklagen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Changed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruecklagen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buchungen",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Betrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Buchungstag = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEinnahme = table.Column<bool>(type: "bit", nullable: false),
                    KategorieId = table.Column<long>(type: "bigint", nullable: true),
                    Changed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buchungen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buchungen_Kategorien_KategorieId",
                        column: x => x.KategorieId,
                        principalTable: "Kategorien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dauerauftraege",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Betrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsEinnahme = table.Column<bool>(type: "bit", nullable: false),
                    Intervall = table.Column<int>(type: "int", nullable: false),
                    Beginn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ende = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KategorieId = table.Column<long>(type: "bigint", nullable: true),
                    Changed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dauerauftraege", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dauerauftraege_Kategorien_KategorieId",
                        column: x => x.KategorieId,
                        principalTable: "Kategorien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buchungen_KategorieId",
                table: "Buchungen",
                column: "KategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Dauerauftraege_KategorieId",
                table: "Dauerauftraege",
                column: "KategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Kategorien_KategorieId",
                table: "Kategorien",
                column: "KategorieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buchungen");

            migrationBuilder.DropTable(
                name: "Dauerauftraege");

            migrationBuilder.DropTable(
                name: "Gutscheine");

            migrationBuilder.DropTable(
                name: "Konfigurationen");

            migrationBuilder.DropTable(
                name: "Ruecklagen");

            migrationBuilder.DropTable(
                name: "Kategorien");
        }
    }
}
