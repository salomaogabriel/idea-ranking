using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeaRanking.Migrations
{
    public partial class AddWins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BiggestRating",
                table: "Ideas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wins",
                table: "Ideas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BiggestRating",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "Ideas");
        }
    }
}
