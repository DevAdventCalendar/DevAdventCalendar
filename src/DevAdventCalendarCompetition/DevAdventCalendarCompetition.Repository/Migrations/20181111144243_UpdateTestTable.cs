using Microsoft.EntityFrameworkCore.Migrations;

namespace DevAdventCalendarCompetition.Repository.Migrations
{
    public partial class UpdateTestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            migrationBuilder.AddColumn<string>(
#pragma warning restore CA1062 // Validate arguments of public methods
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
#pragma warning disable CA1062 // Validate arguments of public methods
            migrationBuilder.DropColumn(
#pragma warning restore CA1062 // Validate arguments of public methods
                name: "Description",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "HashedAnswer",
                table: "Test");
        }
    }
}
