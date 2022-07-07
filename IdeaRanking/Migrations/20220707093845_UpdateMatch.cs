using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeaRanking.Migrations
{
    public partial class UpdateMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PossibleOutcomeIdeaOne",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PossibleOutcomeIdeaTwo",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "History",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PossibleOutcomeIdeaOne",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "PossibleOutcomeIdeaTwo",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "History");
        }
    }
}
