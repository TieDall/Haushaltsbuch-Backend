using Microsoft.EntityFrameworkCore.Migrations;

namespace DataServices.Migrations.MySqlMigrations
{
    public partial class RemoveKategorieSelfReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kategorien_Kategorien_KategorieId",
                table: "Kategorien");

            migrationBuilder.DropIndex(
                name: "IX_Kategorien_KategorieId",
                table: "Kategorien");

            migrationBuilder.DropColumn(
                name: "KategorieId",
                table: "Kategorien");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "KategorieId",
                table: "Kategorien",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kategorien_KategorieId",
                table: "Kategorien",
                column: "KategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kategorien_Kategorien_KategorieId",
                table: "Kategorien",
                column: "KategorieId",
                principalTable: "Kategorien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
