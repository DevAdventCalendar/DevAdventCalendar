using Microsoft.EntityFrameworkCore.Migrations;

namespace DevAdventCalendarCompetition.Repository.Migrations
{
    public partial class NullableResultsFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder != null)
            {
                migrationBuilder.AlterColumn<int>(
                    name: "Week3Points",
                    table: "Results",
                    nullable: true,
                    oldClrType: typeof(int));

                migrationBuilder.AlterColumn<int>(
                    name: "Week3Place",
                    table: "Results",
                    nullable: true,
                    oldClrType: typeof(int));

                migrationBuilder.AlterColumn<int>(
                    name: "Week2Points",
                    table: "Results",
                    nullable: true,
                    oldClrType: typeof(int));

                migrationBuilder.AlterColumn<int>(
                    name: "Week2Place",
                    table: "Results",
                    nullable: true,
                    oldClrType: typeof(int));

                migrationBuilder.AlterColumn<int>(
                    name: "Week1Points",
                    table: "Results",
                    nullable: true,
                    oldClrType: typeof(int));

                migrationBuilder.AlterColumn<int>(
                    name: "Week1Place",
                    table: "Results",
                    nullable: true,
                    oldClrType: typeof(int));

                migrationBuilder.AlterColumn<int>(
                    name: "FinalPoints",
                    table: "Results",
                    nullable: true,
                    oldClrType: typeof(int));

                migrationBuilder.AlterColumn<int>(
                    name: "FinalPlace",
                    table: "Results",
                    nullable: true,
                    oldClrType: typeof(int));
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder != null)
            {
                migrationBuilder.AlterColumn<int>(
                    name: "Week3Points",
                    table: "Results",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "Week3Place",
                    table: "Results",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "Week2Points",
                    table: "Results",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "Week2Place",
                    table: "Results",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "Week1Points",
                    table: "Results",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "Week1Place",
                    table: "Results",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "FinalPoints",
                    table: "Results",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldNullable: true);

                migrationBuilder.AlterColumn<int>(
                    name: "FinalPlace",
                    table: "Results",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldNullable: true);
            }
        }
    }
}
