using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekcik.Migrations
{
    /// <inheritdoc />
    public partial class czyzbyDzialalyRelacje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriesIdKategorii",
                table: "Roboty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Roboty_CategoriesIdKategorii",
                table: "Roboty",
                column: "CategoriesIdKategorii");

            migrationBuilder.AddForeignKey(
                name: "FK_Roboty_Kategorie_CategoriesIdKategorii",
                table: "Roboty",
                column: "CategoriesIdKategorii",
                principalTable: "Kategorie",
                principalColumn: "IdKategorii",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roboty_Kategorie_CategoriesIdKategorii",
                table: "Roboty");

            migrationBuilder.DropIndex(
                name: "IX_Roboty_CategoriesIdKategorii",
                table: "Roboty");

            migrationBuilder.DropColumn(
                name: "CategoriesIdKategorii",
                table: "Roboty");
        }
    }
}
