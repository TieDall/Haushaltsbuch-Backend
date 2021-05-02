using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.MySqlMigrations
{
    public partial class AddKonfigStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Haushaltsbuch.Konfigurationen (Parameter, Wert) values ('Start', 0);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
