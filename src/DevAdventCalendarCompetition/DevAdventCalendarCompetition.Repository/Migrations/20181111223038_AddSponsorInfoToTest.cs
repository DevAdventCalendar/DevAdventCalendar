﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DevAdventCalendarCompetition.Repository.Migrations
{
    public partial class AddSponsorInfoToTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SponsorLogoUrl",
                table: "Test",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SponsorName",
                table: "Test",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SponsorLogoUrl",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "SponsorName",
                table: "Test");
        }
    }
}
