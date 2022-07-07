using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeaRanking.Migrations
{
    public partial class AddNoMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfMatches",
                table: "Ideas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfMatches",
                table: "Ideas");
        }
    }
}
