using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevAdventCalendarCompetition.Repository.Migrations
{
    public partial class WrongAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            migrationBuilder.CreateTable(
                "WrongAnswer",
#pragma warning restore CA1062 // Validate arguments of public methods
                columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                UserId = table.Column<string>(maxLength: 450, nullable: false),
                Time = table.Column<DateTime>(nullable: false),
                Answer = table.Column<string>(nullable: false),
                TestId = table.Column<int>(nullable: false),
            },
                constraints: table =>
            {
                table.PrimaryKey("PK_WrongAnswer", x => x.Id);
                table.ForeignKey(
                      name: "FK_WrongAnswer_Test_TestId",
                      column: x => x.TestId,
                      principalTable: "Test",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_WrongAnswer_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

            migrationBuilder.CreateIndex(
               name: "IX_WrongAnswer_UserId",
               table: "WrongAnswer",
               column: "UserId");

            migrationBuilder.CreateIndex(
              name: "IX_WrongAnswer_TestId",
              table: "WrongAnswer",
              column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            migrationBuilder.DropIndex(
#pragma warning restore CA1062 // Validate arguments of public methods
              name: "IX_WrongAnswer_UserId",
              table: "WrongAnswer");

            migrationBuilder.DropIndex(
              name: "IX_WrongAnswer_TestId",
              table: "WrongAnswer");

            migrationBuilder.DropTable("WrongAnswer");
        }
    }
}