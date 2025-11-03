using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekcik.Migrations
{
    /// <inheritdoc />
    public partial class nn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdZawodnika",
                table: "Druzyny");

            migrationBuilder.CreateTable(
                name: "TeamUsers",
                columns: table => new
                {
                    IdDruzyny = table.Column<int>(type: "int", nullable: false),
                    IdZwodnikaIdZawodnika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamUsers", x => new { x.IdDruzyny, x.IdZwodnikaIdZawodnika });
                    table.ForeignKey(
                        name: "FK_TeamUsers_Druzyny_IdDruzyny",
                        column: x => x.IdDruzyny,
                        principalTable: "Druzyny",
                        principalColumn: "IdDruzyny",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamUsers_Zadownicy_IdZwodnikaIdZawodnika",
                        column: x => x.IdZwodnikaIdZawodnika,
                        principalTable: "Zadownicy",
                        principalColumn: "IdZawodnika",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamUsers_IdZwodnikaIdZawodnika",
                table: "TeamUsers",
                column: "IdZwodnikaIdZawodnika");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamUsers");

            migrationBuilder.AddColumn<int>(
                name: "IdZawodnika",
                table: "Druzyny",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
