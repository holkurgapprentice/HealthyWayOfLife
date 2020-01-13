using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthyWayOfLife.Repository.Migrations
{
    public partial class isarchive_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsArchive",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsArchive",
                table: "Sessions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsArchive",
                table: "Logs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsArchive",
                table: "Configurations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsArchive",
                table: "Biometry",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "Configurations");

            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "Biometry");
        }
    }
}
