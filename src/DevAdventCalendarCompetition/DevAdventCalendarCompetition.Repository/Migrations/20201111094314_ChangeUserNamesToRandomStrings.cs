using Microsoft.EntityFrameworkCore.Migrations;

namespace DevAdventCalendarCompetition.Repository.Migrations
{
    public partial class ChangeUserNamesToRandomStrings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            migrationBuilder.Sql(@"
                    DECLARE @returnStr nvarchar(12) = '' 
                    DECLARE @allowedChars nvarchar(62) = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz';
                    DECLARE @i int = 0;
                    DECLARE @id nvarchar(450);
                    DECLARE @isUniqueName bit;

                    DECLARE users_cursor CURSOR FOR
                    SELECT Id
                    FROM AspNetUsers;

                    OPEN users_cursor
                    FETCH NEXT FROM users_cursor
                    INTO @id

                    WHILE @@FETCH_STATUS = 0
                    BEGIN
	                    SET @isUniqueName = 0;

	                    WHILE @isUniqueName = 0
	                    BEGIN

		                    WHILE (@i < 12)
		                    BEGIN
			                    SET @returnStr = CONCAT(@returnStr, substring(@allowedChars, CONVERT(int, FLOOR(RAND() * LEN(@allowedChars) + 1)), 1));
			                    SET @i = @i + 1;
		                    END;

		                    IF NOT EXISTS (SELECT UserName FROM AspNetUsers WHERE UserName = @returnStr)
		                    BEGIN
			                    SET @isUniqueName = 1;

			                    UPDATE AspNetUsers SET UserName = @returnStr, NormalizedUserName = UPPER(@returnStr)
			                    WHERE Id = @id

			                    FETCH NEXT FROM users_cursor
			                    INTO @Id
			                    
			                    SET @i = 0;
			                    SET @returnStr = '';
		                    END;  

	                    END;
                    END;
                    CLOSE users_cursor;
                    DEALLOCATE users_cursor;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                 UPDATE AspNetUsers SET UserName = Email, NormalizedUserName = UPPER(Email)
            ");
#pragma warning restore CA1062 // Validate arguments of public methods
        }
    }
}
