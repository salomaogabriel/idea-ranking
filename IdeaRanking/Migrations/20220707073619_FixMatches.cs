using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeaRanking.Migrations
{
    public partial class FixMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Ideas_IdeaId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_IdeaId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IdeaId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "Winner",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryIdea",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    IdeasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryIdea", x => new { x.CategoriesId, x.IdeasId });
                    table.ForeignKey(
                        name: "FK_CategoryIdea_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryIdea_Ideas_IdeasId",
                        column: x => x.IdeasId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryIdea_IdeasId",
                table: "CategoryIdea",
                column: "IdeasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryIdea");

            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "IdeaId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IdeaId",
                table: "Categories",
                column: "IdeaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Ideas_IdeaId",
                table: "Categories",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id");
        }
    }
}
