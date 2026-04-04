using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStatistics.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class PlayerLeftClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeletedOnUtc",
                schema: "sports",
                table: "Players",
                newName: "LeftClubOnUtc");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                schema: "sports",
                table: "Players",
                newName: "LeftClub");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LeftClubOnUtc",
                schema: "sports",
                table: "Players",
                newName: "DeletedOnUtc");

            migrationBuilder.RenameColumn(
                name: "LeftClub",
                schema: "sports",
                table: "Players",
                newName: "Deleted");
        }
    }
}
