using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportStats.Migrations
{
    /// <inheritdoc />
    public partial class ModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Stats");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Stats",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Stats");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Stats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
