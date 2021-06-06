using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.MsSqlMigrations
{
    public partial class CreatedChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "Ruecklagen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Ruecklagen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "ReportRows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ReportRows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "ReportItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ReportItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "Konfigurationen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Konfigurationen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "Kategorien",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Kategorien",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "Gutscheine",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Gutscheine",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "Dauerauftraege",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Dauerauftraege",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "Buchungen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Buchungen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Changed",
                table: "Ruecklagen");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Ruecklagen");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "ReportRows");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ReportRows");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "ReportItems");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ReportItems");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "Konfigurationen");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Konfigurationen");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "Kategorien");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Kategorien");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "Gutscheine");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Gutscheine");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "Dauerauftraege");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Dauerauftraege");

            migrationBuilder.DropColumn(
                name: "Changed",
                table: "Buchungen");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Buchungen");
        }
    }
}
