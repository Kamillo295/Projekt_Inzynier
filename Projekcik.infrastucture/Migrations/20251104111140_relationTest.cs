using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekcik.Migrations
{
    /// <inheritdoc />
    public partial class relationTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Druzyny_ZawodnicyIdDruzyny",
                table: "TeamUsers");

            migrationBuilder.RenameColumn(
                name: "ZawodnicyIdDruzyny",
                table: "TeamUsers",
                newName: "DruzynyIdDruzyny");

            migrationBuilder.AddColumn<int>(
                name: "IdDruzyny",
                table: "Roboty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamIdDruzyny",
                table: "Roboty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Roboty_TeamIdDruzyny",
                table: "Roboty",
                column: "TeamIdDruzyny");

            migrationBuilder.AddForeignKey(
                name: "FK_Roboty_Druzyny_TeamIdDruzyny",
                table: "Roboty",
                column: "TeamIdDruzyny",
                principalTable: "Druzyny",
                principalColumn: "IdDruzyny",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Druzyny_DruzynyIdDruzyny",
                table: "TeamUsers",
                column: "DruzynyIdDruzyny",
                principalTable: "Druzyny",
                principalColumn: "IdDruzyny",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roboty_Druzyny_TeamIdDruzyny",
                table: "Roboty");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamUsers_Druzyny_DruzynyIdDruzyny",
                table: "TeamUsers");

            migrationBuilder.DropIndex(
                name: "IX_Roboty_TeamIdDruzyny",
                table: "Roboty");

            migrationBuilder.DropColumn(
                name: "IdDruzyny",
                table: "Roboty");

            migrationBuilder.DropColumn(
                name: "TeamIdDruzyny",
                table: "Roboty");

            migrationBuilder.RenameColumn(
                name: "DruzynyIdDruzyny",
                table: "TeamUsers",
                newName: "ZawodnicyIdDruzyny");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUsers_Druzyny_ZawodnicyIdDruzyny",
                table: "TeamUsers",
                column: "ZawodnicyIdDruzyny",
                principalTable: "Druzyny",
                principalColumn: "IdDruzyny",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
