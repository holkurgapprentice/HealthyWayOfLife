using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthyWayOfLife.Repository.Migrations
{
    public partial class Loging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogType",
                table: "Logs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogType",
                table: "Logs");
        }
    }
}
