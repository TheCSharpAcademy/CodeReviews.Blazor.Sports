using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportStats.Migrations
{
    /// <inheritdoc />
    public partial class updatemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInTeam",
                table: "Players");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInTeam",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
