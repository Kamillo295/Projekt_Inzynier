using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekcik.Migrations
{
    /// <inheritdoc />
    public partial class userRobot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRobota",
                table: "Zawodnicy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Zawodnicy_IdRobota",
                table: "Zawodnicy",
                column: "IdRobota");

            migrationBuilder.AddForeignKey(
                name: "FK_Zawodnicy_Roboty_IdRobota",
                table: "Zawodnicy",
                column: "IdRobota",
                principalTable: "Roboty",
                principalColumn: "IdRobota",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zawodnicy_Roboty_IdRobota",
                table: "Zawodnicy");

            migrationBuilder.DropIndex(
                name: "IX_Zawodnicy_IdRobota",
                table: "Zawodnicy");

            migrationBuilder.DropColumn(
                name: "IdRobota",
                table: "Zawodnicy");
        }
    }
}
