using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStatistics.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedMatchTrackingMatchPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Minute",
                schema: "sports",
                table: "SubstitutionEvents",
                newName: "period");

            migrationBuilder.RenameColumn(
                name: "Minute",
                schema: "sports",
                table: "PlayerEvents",
                newName: "period");

            migrationBuilder.RenameColumn(
                name: "Minute",
                schema: "sports",
                table: "MatchEvents",
                newName: "period");

            migrationBuilder.AddColumn<int>(
                name: "minute_base",
                schema: "sports",
                table: "SubstitutionEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "minute_stoppage",
                schema: "sports",
                table: "SubstitutionEvents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "minute_base",
                schema: "sports",
                table: "PlayerEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "minute_stoppage",
                schema: "sports",
                table: "PlayerEvents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "minute_base",
                schema: "sports",
                table: "MatchEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "minute_stoppage",
                schema: "sports",
                table: "MatchEvents",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "minute_base",
                schema: "sports",
                table: "SubstitutionEvents");

            migrationBuilder.DropColumn(
                name: "minute_stoppage",
                schema: "sports",
                table: "SubstitutionEvents");

            migrationBuilder.DropColumn(
                name: "minute_base",
                schema: "sports",
                table: "PlayerEvents");

            migrationBuilder.DropColumn(
                name: "minute_stoppage",
                schema: "sports",
                table: "PlayerEvents");

            migrationBuilder.DropColumn(
                name: "minute_base",
                schema: "sports",
                table: "MatchEvents");

            migrationBuilder.DropColumn(
                name: "minute_stoppage",
                schema: "sports",
                table: "MatchEvents");

            migrationBuilder.RenameColumn(
                name: "period",
                schema: "sports",
                table: "SubstitutionEvents",
                newName: "Minute");

            migrationBuilder.RenameColumn(
                name: "period",
                schema: "sports",
                table: "PlayerEvents",
                newName: "Minute");

            migrationBuilder.RenameColumn(
                name: "period",
                schema: "sports",
                table: "MatchEvents",
                newName: "Minute");
        }
    }
}
