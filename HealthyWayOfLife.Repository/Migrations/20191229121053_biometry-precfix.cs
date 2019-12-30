using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthyWayOfLife.Repository.Migrations
{
    public partial class biometryprecfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DecimalValue",
                table: "Configurations",
                type: "decimal(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "WeightInKgs",
                table: "Biometry",
                type: "decimal(19,4)",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "WaistInCm",
                table: "Biometry",
                type: "decimal(19,4)",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "LegInCm",
                table: "Biometry",
                type: "decimal(19,4)",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "HeightInCm",
                table: "Biometry",
                type: "decimal(19,4)",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "ChestInCm",
                table: "Biometry",
                type: "decimal(19,4)",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                name: "ArmInCm",
                table: "Biometry",
                type: "decimal(19,4)",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DecimalValue",
                table: "Configurations",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "WeightInKgs",
                table: "Biometry",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)");

            migrationBuilder.AlterColumn<float>(
                name: "WaistInCm",
                table: "Biometry",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)");

            migrationBuilder.AlterColumn<float>(
                name: "LegInCm",
                table: "Biometry",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)");

            migrationBuilder.AlterColumn<float>(
                name: "HeightInCm",
                table: "Biometry",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)");

            migrationBuilder.AlterColumn<float>(
                name: "ChestInCm",
                table: "Biometry",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)");

            migrationBuilder.AlterColumn<float>(
                name: "ArmInCm",
                table: "Biometry",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)");
        }
    }
}
