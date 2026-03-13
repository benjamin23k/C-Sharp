using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tareaOpcional.Migrations
{
    /// <inheritdoc />
    public partial class AddUniquePersonalIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Personal",
                table: "Patient",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Personal",
                table: "Patient",
                column: "Personal",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patient_Personal",
                table: "Patient");

            migrationBuilder.AlterColumn<string>(
                name: "Personal",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
