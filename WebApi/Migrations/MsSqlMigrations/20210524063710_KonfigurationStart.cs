using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations.MsSqlMigrations
{
    public partial class KonfigurationStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Konfigurationen(Parameter, Wert) VALUES ('Start', 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
