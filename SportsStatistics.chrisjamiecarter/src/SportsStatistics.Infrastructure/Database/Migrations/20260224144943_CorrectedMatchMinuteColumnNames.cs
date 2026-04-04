using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStatistics.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedMatchMinuteColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "period",
                schema: "sports",
                table: "SubstitutionEvents");

            migrationBuilder.DropColumn(
                name: "period",
                schema: "sports",
                table: "PlayerEvents");

            migrationBuilder.DropColumn(
                name: "period",
                schema: "sports",
                table: "MatchEvents");

            migrationBuilder.RenameColumn(
                name: "minute_stoppage",
                schema: "sports",
                table: "SubstitutionEvents",
                newName: "StoppageMinute");

            migrationBuilder.RenameColumn(
                name: "minute_base",
                schema: "sports",
                table: "SubstitutionEvents",
                newName: "BaseMinute");

            migrationBuilder.RenameColumn(
                name: "minute_stoppage",
                schema: "sports",
                table: "PlayerEvents",
                newName: "StoppageMinute");

            migrationBuilder.RenameColumn(
                name: "minute_base",
                schema: "sports",
                table: "PlayerEvents",
                newName: "BaseMinute");

            migrationBuilder.RenameColumn(
                name: "minute_stoppage",
                schema: "sports",
                table: "MatchEvents",
                newName: "StoppageMinute");

            migrationBuilder.RenameColumn(
                name: "minute_base",
                schema: "sports",
                table: "MatchEvents",
                newName: "BaseMinute");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoppageMinute",
                schema: "sports",
                table: "SubstitutionEvents",
                newName: "minute_stoppage");

            migrationBuilder.RenameColumn(
                name: "BaseMinute",
                schema: "sports",
                table: "SubstitutionEvents",
                newName: "minute_base");

            migrationBuilder.RenameColumn(
                name: "StoppageMinute",
                schema: "sports",
                table: "PlayerEvents",
                newName: "minute_stoppage");

            migrationBuilder.RenameColumn(
                name: "BaseMinute",
                schema: "sports",
                table: "PlayerEvents",
                newName: "minute_base");

            migrationBuilder.RenameColumn(
                name: "StoppageMinute",
                schema: "sports",
                table: "MatchEvents",
                newName: "minute_stoppage");

            migrationBuilder.RenameColumn(
                name: "BaseMinute",
                schema: "sports",
                table: "MatchEvents",
                newName: "minute_base");

            migrationBuilder.AddColumn<int>(
                name: "period",
                schema: "sports",
                table: "SubstitutionEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "period",
                schema: "sports",
                table: "PlayerEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "period",
                schema: "sports",
                table: "MatchEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
