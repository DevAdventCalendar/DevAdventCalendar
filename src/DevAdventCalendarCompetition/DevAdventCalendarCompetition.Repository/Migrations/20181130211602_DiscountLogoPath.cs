using Microsoft.EntityFrameworkCore.Migrations;

namespace DevAdventCalendarCompetition.Repository.Migrations
{
#pragma warning disable SA1601 // Partial elements should be documented
    public partial class DiscountLogoPath : Migration
#pragma warning restore SA1601 // Partial elements should be documented
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            migrationBuilder.AddColumn<string>(
#pragma warning restore CA1062 // Validate arguments of public methods
                name: "DiscountLogoPath",
                table: "Test",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            migrationBuilder.DropColumn(
#pragma warning restore CA1062 // Validate arguments of public methods
                name: "DiscountLogoPath",
                table: "Test");
        }
    }
}
