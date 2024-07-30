using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportStats.Migrations
{
    /// <inheritdoc />
    public partial class modelupdate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_MemberOfId",
                table: "Players");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_MemberOfId",
                table: "Players",
                column: "MemberOfId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_MemberOfId",
                table: "Players");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_MemberOfId",
                table: "Players",
                column: "MemberOfId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
