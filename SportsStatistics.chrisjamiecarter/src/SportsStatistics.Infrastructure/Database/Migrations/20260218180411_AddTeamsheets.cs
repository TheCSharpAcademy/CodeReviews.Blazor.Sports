using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStatistics.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamsheets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teamsheets",
                schema: "sports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FixtureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmittedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teamsheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamsheetPlayers",
                schema: "sports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamsheetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsStarter = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsheetPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamsheetPlayers_Teamsheets_TeamsheetId",
                        column: x => x.TeamsheetId,
                        principalSchema: "sports",
                        principalTable: "Teamsheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamsheetPlayers_TeamsheetId_PlayerId",
                schema: "sports",
                table: "TeamsheetPlayers",
                columns: ["TeamsheetId", "PlayerId"],
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teamsheets_FixtureId",
                schema: "sports",
                table: "Teamsheets",
                column: "FixtureId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamsheetPlayers",
                schema: "sports");

            migrationBuilder.DropTable(
                name: "Teamsheets",
                schema: "sports");
        }
    }
}
