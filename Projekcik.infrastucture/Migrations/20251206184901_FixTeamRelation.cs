using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekcik.Migrations
{
    /// <inheritdoc />
    public partial class FixTeamRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roboty_Druzyny_TeamIdDruzyny",
                table: "Roboty");

            migrationBuilder.DropForeignKey(
                name: "FK_Roboty_Kategorie_CategoriesIdKategorii",
                table: "Roboty");

            migrationBuilder.DropForeignKey(
                name: "FK_Roboty_Zawodnicy_IdZawodnika",
                table: "Roboty");

            migrationBuilder.DropIndex(
                name: "IX_Roboty_CategoriesIdKategorii",
                table: "Roboty");

            migrationBuilder.DropIndex(
                name: "IX_Roboty_TeamIdDruzyny",
                table: "Roboty");

            migrationBuilder.DropColumn(
                name: "CategoriesIdKategorii",
                table: "Roboty");

            migrationBuilder.DropColumn(
                name: "TeamIdDruzyny",
                table: "Roboty");

            migrationBuilder.AlterColumn<int>(
                name: "IdZawodnika",
                table: "Roboty",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roboty_IdDruzyny",
                table: "Roboty",
                column: "IdDruzyny");

            migrationBuilder.CreateIndex(
                name: "IX_Roboty_IdKategorii",
                table: "Roboty",
                column: "IdKategorii");

            migrationBuilder.AddForeignKey(
                name: "FK_Roboty_Druzyny_IdDruzyny",
                table: "Roboty",
                column: "IdDruzyny",
                principalTable: "Druzyny",
                principalColumn: "IdDruzyny",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roboty_Kategorie_IdKategorii",
                table: "Roboty",
                column: "IdKategorii",
                principalTable: "Kategorie",
                principalColumn: "IdKategorii",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roboty_Zawodnicy_IdZawodnika",
                table: "Roboty",
                column: "IdZawodnika",
                principalTable: "Zawodnicy",
                principalColumn: "IdZawodnika",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roboty_Druzyny_IdDruzyny",
                table: "Roboty");

            migrationBuilder.DropForeignKey(
                name: "FK_Roboty_Kategorie_IdKategorii",
                table: "Roboty");

            migrationBuilder.DropForeignKey(
                name: "FK_Roboty_Zawodnicy_IdZawodnika",
                table: "Roboty");

            migrationBuilder.DropIndex(
                name: "IX_Roboty_IdDruzyny",
                table: "Roboty");

            migrationBuilder.DropIndex(
                name: "IX_Roboty_IdKategorii",
                table: "Roboty");

            migrationBuilder.AlterColumn<int>(
                name: "IdZawodnika",
                table: "Roboty",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoriesIdKategorii",
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
                name: "IX_Roboty_CategoriesIdKategorii",
                table: "Roboty",
                column: "CategoriesIdKategorii");

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
                name: "FK_Roboty_Kategorie_CategoriesIdKategorii",
                table: "Roboty",
                column: "CategoriesIdKategorii",
                principalTable: "Kategorie",
                principalColumn: "IdKategorii",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roboty_Zawodnicy_IdZawodnika",
                table: "Roboty",
                column: "IdZawodnika",
                principalTable: "Zawodnicy",
                principalColumn: "IdZawodnika");
        }
    }
}
