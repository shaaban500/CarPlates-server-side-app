using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPlates.Migrations
{
    /// <inheritdoc />
    public partial class ReturnedPlatestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReturnedPlates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarPlateId = table.Column<long>(type: "bigint", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CommitteeNumber = table.Column<int>(type: "int", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnedPlates");
        }
    }
}
