using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekcik.Migrations
{
    /// <inheritdoc />
    public partial class INT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NumerTelefonu",
                table: "Zawodnicy",
                type: "int",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NumerTelefonu",
                table: "Zawodnicy",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 12);
        }
    }
}
