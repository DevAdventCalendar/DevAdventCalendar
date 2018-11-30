using Microsoft.EntityFrameworkCore.Migrations;

namespace DevAdventCalendarCompetition.Repository.Migrations
{
    public partial class DiscountUrlAndLogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiscountLogoUrl",
                table: "Test",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscountUrl",
                table: "Test",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountLogoUrl",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "DiscountUrl",
                table: "Test");
        }
    }
}
