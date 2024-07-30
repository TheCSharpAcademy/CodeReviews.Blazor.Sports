using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportStats.Migrations
{
    /// <inheritdoc />
    public partial class teammodelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnTeamId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_OwnTeamId",
                table: "Games",
                column: "OwnTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_OwnTeamId",
                table: "Games",
                column: "OwnTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_OwnTeamId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_OwnTeamId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "OwnTeamId",
                table: "Games");
        }
    }
}
