using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthyWayOfLife.Repository.Migrations
{
    public partial class biometry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Biometry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsertBy = table.Column<int>(nullable: false),
                    UpdateBy = table.Column<int>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    WeightInKgs = table.Column<float>(nullable: false),
                    HeightInCm = table.Column<float>(nullable: false),
                    ChestInCm = table.Column<float>(nullable: false),
                    ArmInCm = table.Column<float>(nullable: false),
                    LegInCm = table.Column<float>(nullable: false),
                    WaistInCm = table.Column<float>(nullable: false),
                    DateFor = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biometry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biometry_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biometry_UserId",
                table: "Biometry",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biometry");
        }
    }
}
