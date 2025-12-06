using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekcik.Migrations
{
    /// <inheritdoc />
    public partial class zmianaTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdZawodnika",
                table: "Roboty",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roboty_IdZawodnika",
                table: "Roboty",
                column: "IdZawodnika");

            migrationBuilder.AddForeignKey(
                name: "FK_Roboty_Zawodnicy_IdZawodnika",
                table: "Roboty",
                column: "IdZawodnika",
                principalTable: "Zawodnicy",
                principalColumn: "IdZawodnika");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roboty_Zawodnicy_IdZawodnika",
                table: "Roboty");

            migrationBuilder.DropIndex(
                name: "IX_Roboty_IdZawodnika",
                table: "Roboty");

            migrationBuilder.DropColumn(
                name: "IdZawodnika",
                table: "Roboty");
        }
    }
}
