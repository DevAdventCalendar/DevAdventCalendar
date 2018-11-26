using Microsoft.EntityFrameworkCore.Migrations;

namespace DevAdventCalendarCompetition.Repository.Migrations
{
    public partial class UpdateTestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Test",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashedAnswer",
                table: "Test",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "HashedAnswer",
                table: "Test");
        }
    }
}
