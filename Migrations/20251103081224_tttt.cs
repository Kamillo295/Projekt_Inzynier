using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekcik.Migrations
{
    /// <inheritdoc />
    public partial class tttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zadownicy_Druzyny_TeamIdDruzyny",
                table: "Zadownicy");

            migrationBuilder.DropIndex(
                name: "IX_Zadownicy_TeamIdDruzyny",
                table: "Zadownicy");

            migrationBuilder.DropColumn(
                name: "TeamIdDruzyny",
                table: "Zadownicy");

            migrationBuilder.CreateTable(
                name: "TeamUsers",
                columns: table => new
                {
                    ZawodnicyIdDruzyny = table.Column<int>(type: "int", nullable: false),
                    ZawodnicyIdZawodnika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamUsers", x => new { x.ZawodnicyIdDruzyny, x.ZawodnicyIdZawodnika });
                    table.ForeignKey(
                        name: "FK_TeamUsers_Druzyny_ZawodnicyIdDruzyny",
                        column: x => x.ZawodnicyIdDruzyny,
                        principalTable: "Druzyny",
                        principalColumn: "IdDruzyny",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamUsers_Zadownicy_ZawodnicyIdZawodnika",
                        column: x => x.ZawodnicyIdZawodnika,
                        principalTable: "Zadownicy",
                        principalColumn: "IdZawodnika",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamUsers_ZawodnicyIdZawodnika",
                table: "TeamUsers",
                column: "ZawodnicyIdZawodnika");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamUsers");

            migrationBuilder.AddColumn<int>(
                name: "TeamIdDruzyny",
                table: "Zadownicy",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zadownicy_TeamIdDruzyny",
                table: "Zadownicy",
                column: "TeamIdDruzyny");

            migrationBuilder.AddForeignKey(
                name: "FK_Zadownicy_Druzyny_TeamIdDruzyny",
                table: "Zadownicy",
                column: "TeamIdDruzyny",
                principalTable: "Druzyny",
                principalColumn: "IdDruzyny");
        }
    }
}
