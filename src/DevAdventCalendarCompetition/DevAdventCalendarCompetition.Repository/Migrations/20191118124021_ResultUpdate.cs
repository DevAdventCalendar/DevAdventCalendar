using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevAdventCalendarCompetition.Repository.Migrations
{
    public partial class ResultUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder != null)
            {
                migrationBuilder.CreateTable(
                    name: "Results",
                    columns: table => new
                    {
                        Id = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                        UserId = table.Column<string>(maxLength: 450, nullable: false),
                        Week1Points = table.Column<int>(nullable: false),
                        Week1Place = table.Column<int>(nullable: false),
                        Week2Points = table.Column<int>(nullable: false),
                        Week2Place = table.Column<int>(nullable: false),
                        Week3Points = table.Column<int>(nullable: false),
                        Week3Place = table.Column<int>(nullable: false),
                        FinalPoints = table.Column<int>(nullable: false),
                        FinalPlace = table.Column<int>(nullable: false),
                        CorrectAnswersCount = table.Column<int>(nullable: false),
                        WrongAnswersCount = table.Column<int>(nullable: false),
                        Points = table.Column<int>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Results", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Results_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_Results_UserId",
                    table: "Results",
                    column: "UserId");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder != null)
            {
                migrationBuilder.DropTable("Results");
            }
        }
    }
}
