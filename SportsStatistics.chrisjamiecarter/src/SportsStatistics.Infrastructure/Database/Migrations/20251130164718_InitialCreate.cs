using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStatistics.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sports");

            migrationBuilder.CreateTable(
                name: "Players",
                schema: "sports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SquadNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                schema: "sports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubstitutionEvents",
                schema: "sports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerOffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerOnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    FixtureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubstitutionEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubstitutionEvents_Players_PlayerOffId",
                        column: x => x.PlayerOffId,
                        principalSchema: "sports",
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubstitutionEvents_Players_PlayerOnId",
                        column: x => x.PlayerOnId,
                        principalSchema: "sports",
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                schema: "sports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Format = table.Column<int>(type: "int", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competitions_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalSchema: "sports",
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fixtures",
                schema: "sports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    HomeGoals = table.Column<int>(type: "int", nullable: false),
                    AwayGoals = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    KickoffTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Opponent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixtures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fixtures_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalSchema: "sports",
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchEvents",
                schema: "sports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    FixtureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchEvents_Fixtures_FixtureId",
                        column: x => x.FixtureId,
                        principalSchema: "sports",
                        principalTable: "Fixtures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerEvents",
                schema: "sports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false),
                    FixtureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerEvents_Fixtures_FixtureId",
                        column: x => x.FixtureId,
                        principalSchema: "sports",
                        principalTable: "Fixtures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerEvents_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "sports",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_SeasonId",
                schema: "sports",
                table: "Competitions",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_CompetitionId",
                schema: "sports",
                table: "Fixtures",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchEvents_FixtureId",
                schema: "sports",
                table: "MatchEvents",
                column: "FixtureId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerEvents_FixtureId",
                schema: "sports",
                table: "PlayerEvents",
                column: "FixtureId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerEvents_PlayerId",
                schema: "sports",
                table: "PlayerEvents",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutionEvents_PlayerOffId",
                schema: "sports",
                table: "SubstitutionEvents",
                column: "PlayerOffId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutionEvents_PlayerOnId",
                schema: "sports",
                table: "SubstitutionEvents",
                column: "PlayerOnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchEvents",
                schema: "sports");

            migrationBuilder.DropTable(
                name: "PlayerEvents",
                schema: "sports");

            migrationBuilder.DropTable(
                name: "SubstitutionEvents",
                schema: "sports");

            migrationBuilder.DropTable(
                name: "Fixtures",
                schema: "sports");

            migrationBuilder.DropTable(
                name: "Players",
                schema: "sports");

            migrationBuilder.DropTable(
                name: "Competitions",
                schema: "sports");

            migrationBuilder.DropTable(
                name: "Seasons",
                schema: "sports");
        }
    }
}
