using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPlates.Migrations
{
    /// <inheritdoc />
    public partial class initialdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarStates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarPlates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Letters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerAdress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerNationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CarTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CarStateId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarPlates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarPlates_CarStates_CarStateId",
                        column: x => x.CarStateId,
                        principalTable: "CarStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarPlates_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarPlates_CarStateId",
                table: "CarPlates",
                column: "CarStateId");

            migrationBuilder.CreateIndex(
                name: "IX_CarPlates_CarTypeId",
                table: "CarPlates",
                column: "CarTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarPlates");

            migrationBuilder.DropTable(
                name: "CarStates");

            migrationBuilder.DropTable(
                name: "CarTypes");
        }
    }
}
