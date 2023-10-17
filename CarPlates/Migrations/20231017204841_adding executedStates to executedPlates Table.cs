using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPlates.Migrations
{
    /// <inheritdoc />
    public partial class addingexecutedStatestoexecutedPlatesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExecutedCarStateId",
                table: "ExecutedPlates",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ExecutedPlates_ExecutedCarStateId",
                table: "ExecutedPlates",
                column: "ExecutedCarStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutedPlates_ExecutedCarStates_ExecutedCarStateId",
                table: "ExecutedPlates",
                column: "ExecutedCarStateId",
                principalTable: "ExecutedCarStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutedPlates_ExecutedCarStates_ExecutedCarStateId",
                table: "ExecutedPlates");

            migrationBuilder.DropIndex(
                name: "IX_ExecutedPlates_ExecutedCarStateId",
                table: "ExecutedPlates");

            migrationBuilder.DropColumn(
                name: "ExecutedCarStateId",
                table: "ExecutedPlates");
        }
    }
}
