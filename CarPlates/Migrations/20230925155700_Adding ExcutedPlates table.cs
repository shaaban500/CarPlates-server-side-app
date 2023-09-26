using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPlates.Migrations
{
    /// <inheritdoc />
    public partial class AddingExcutedPlatestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnedPlates");

            migrationBuilder.CreateTable(
                name: "ExecutedPlates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Letters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutionYear = table.Column<int>(type: "int", nullable: false),
                    ExecutionNumber = table.Column<int>(type: "int", nullable: false),
                    CarTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutedPlates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutedPlates_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExecutedPlates_CarTypeId",
                table: "ExecutedPlates",
                column: "CarTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExecutedPlates");

            migrationBuilder.CreateTable(
                name: "ReturnedPlates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarPlateId = table.Column<long>(type: "bigint", nullable: false),
                    CommitteeNumber = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnedPlates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnedPlates_CarPlates_CarPlateId",
                        column: x => x.CarPlateId,
                        principalTable: "CarPlates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedPlates_CarPlateId",
                table: "ReturnedPlates",
                column: "CarPlateId");
        }
    }
}
