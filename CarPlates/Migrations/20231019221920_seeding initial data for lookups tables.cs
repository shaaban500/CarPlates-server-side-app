using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarPlates.Migrations
{
    /// <inheritdoc />
    public partial class seedinginitialdataforlookupstables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CarStates",
                columns: new[] { "Id", "IsDeleted", "State" },
                values: new object[,]
                {
                    { 1L, false, "بالحركة" },
                    { 2L, false, "مرتجع سليم" },
                    { 3L, false, "سليم" }
                });

            migrationBuilder.InsertData(
                table: "CarTypes",
                columns: new[] { "Id", "IsDeleted", "Type" },
                values: new object[,]
                {
                    { 1L, false, "نقل" },
                    { 2L, false, "ملاكي" },
                    { 3L, false, "أجرة" },
                    { 4L, false, "مقطورة" },
                    { 5L, false, "حكومة" },
                    { 6L, false, "قطاع عام" },
                    { 7L, false, "معدة ثقيلة" },
                    { 8L, false, "محافظة" },
                    { 9L, false, "دراجة محافظة" },
                    { 10L, false, "تحت الطلب" },
                    { 11L, false, "مؤقت" },
                    { 12L, false, "جرار زراعي" },
                    { 13L, false, "مقطورة قطاع عام" },
                    { 14L, false, "أتوبيس عام" },
                    { 15L, false, "أتوبيس خاص" },
                    { 16L, false, "أتوبيس مدارس" },
                    { 17L, false, "أتوبيس رحلات" },
                    { 18L, false, "تجاري" },
                    { 19L, false, "دراجة" },
                    { 20L, false, "دراجة أجرة" },
                    { 21L, false, "أتوبيس سياحة" },
                    { 22L, false, "سياحة" },
                    { 23L, false, "مقطورة محافظة" },
                    { 24L, false, "دراجة حكومة" },
                    { 25L, false, "دراجة قطاع عام" },
                    { 26L, false, "مقطورة حكومة" },
                    { 27L, false, "ملحقة" }
                });

            migrationBuilder.InsertData(
                table: "ExecutedCarStates",
                columns: new[] { "Id", "IsDeleted", "State" },
                values: new object[,]
                {
                    { 1L, false, "مرتجع تالف" },
                    { 2L, false, "مرتجع فاقد بالزوج" },
                    { 3L, false, "مرتجع فاقد بالفرد" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarStates",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "CarStates",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "CarStates",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "CarTypes",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "ExecutedCarStates",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ExecutedCarStates",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ExecutedCarStates",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
