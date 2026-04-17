using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class AllowDuplicateOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_plate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "order",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_plate",
                table: "Orders",
                column: "plate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_plate",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "order",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_plate",
                table: "Orders",
                column: "plate",
                unique: true,
                filter: "[plate] IS NOT NULL");
        }
    }
}
