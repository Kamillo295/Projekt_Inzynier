using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekcik.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Zadownicy_ZawodnicyIdZawodnika",
                table: "TeamUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zadownicy",
                table: "Zadownicy");

            migrationBuilder.RenameTable(
                name: "Zadownicy",
                newName: "Zawodnicy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zawodnicy",
                table: "Zawodnicy",
                column: "IdZawodnika");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Zawodnicy_ZawodnicyIdZawodnika",
                table: "TeamUsers",
                column: "ZawodnicyIdZawodnika",
                principalTable: "Zawodnicy",
                principalColumn: "IdZawodnika",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Zawodnicy_ZawodnicyIdZawodnika",
                table: "TeamUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zawodnicy",
                table: "Zawodnicy");

            migrationBuilder.RenameTable(
                name: "Zawodnicy",
                newName: "Zadownicy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zadownicy",
                table: "Zadownicy",
                column: "IdZawodnika");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Zadownicy_ZawodnicyIdZawodnika",
                table: "TeamUsers",
                column: "ZawodnicyIdZawodnika",
                principalTable: "Zadownicy",
                principalColumn: "IdZawodnika",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
