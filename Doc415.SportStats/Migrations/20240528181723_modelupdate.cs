using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportStats.Migrations
{
    /// <inheritdoc />
    public partial class modelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_MemberOfId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Stats");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Stats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "MemberOfId",
                table: "Players",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_MemberOfId",
                table: "Players",
                column: "MemberOfId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_MemberOfId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Stats");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Stats",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AlterColumn<int>(
                name: "MemberOfId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_MemberOfId",
                table: "Players",
                column: "MemberOfId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
