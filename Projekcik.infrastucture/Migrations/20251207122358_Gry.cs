using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekcik.Migrations
{
    /// <inheritdoc />
    public partial class Gry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gry",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Robot1ID = table.Column<int>(type: "int", nullable: false),
                    Robot2ID = table.Column<int>(type: "int", nullable: false),
                    Zwyciezca = table.Column<int>(type: "int", nullable: false),
                    StopienDrabinki = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Gry_Roboty_Robot1ID",
                        column: x => x.Robot1ID,
                        principalTable: "Roboty",
                        principalColumn: "IdRobota",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gry_Roboty_Robot2ID",
                        column: x => x.Robot2ID,
                        principalTable: "Roboty",
                        principalColumn: "IdRobota",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gry_Robot1ID",
                table: "Gry",
                column: "Robot1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Gry_Robot2ID",
                table: "Gry",
                column: "Robot2ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gry");
        }
    }
}
