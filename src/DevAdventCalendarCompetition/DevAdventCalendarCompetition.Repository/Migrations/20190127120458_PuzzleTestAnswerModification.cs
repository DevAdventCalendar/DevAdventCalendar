using Microsoft.EntityFrameworkCore.Migrations;

namespace DevAdventCalendarCompetition.Repository.Migrations
{
    public partial class PuzzleTestAnswerModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlainAnswer",
                table: "Test");

            migrationBuilder.AddColumn<string>(
                name: "PlainAnswer",
                table: "TestAnswer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlainAnswer",
                table: "TestAnswer");

            migrationBuilder.AddColumn<string>(
                name: "PlainAnswer",
                table: "Test",
                nullable: true);
        }
    }
}
