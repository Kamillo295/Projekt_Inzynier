using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekcik.Migrations
{
    /// <inheritdoc />
    public partial class AddRobotClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Zadownicy",
                newName: "IdZawodnika");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdZawodnika",
                table: "Zadownicy",
                newName: "Id");
        }
    }
}
