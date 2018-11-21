using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DevAdventCalendarCompetition.Repository.Migrations
{
	public partial class WrongAnswer : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable("WrongAnswer",
			columns: table => new
			{
				Id = table.Column<int>(nullable: false)
					.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
				UserId = table.Column<string>(maxLength: 450, nullable: false),
				Time = table.Column<DateTime>(nullable: false),
				Answer = table.Column<string>(nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_WrongAnswer", x => x.Id);
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
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
			  name: "IX_WrongAnswer_UserId",
			  table: "WrongAnswer");

			migrationBuilder.DropTable("WrongAnswer");
		}
	}
}
